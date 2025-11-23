using AusguckBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Text.Json;

namespace AusguckBackend.Services
{
    public class RegistrationService : IRegistrationService
    {
        public static readonly List<string> mandatoryNames = ["lastName", "firstName", "dateOfBirth", "gender", "zipCode", "city", "streetAndNumber", "email", "schoolType", "shirtSize", "hasLiabilityInsurance", "perms", "swimmer", "selectedSlot", "nutrition", "isHealthy", "needsMedication", "picturesAllowed", "question1"];

        NpgsqlConnection conn = new (Globals.ConnectionString);
        
        public RegistrationService()
        {
            conn.Open();
        }

        public Task ProcessIncomingDataAsync(InParticipant data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), "Input data cannot be null");
            }
            string verificationResult = Verify(data);
            if (verificationResult != "")
            {
                throw new ArgumentException("Input data verification failed: " + verificationResult);
            }
            //InsertParticipant(data);

            return Task.CompletedTask;
        }

        public string Verify(InParticipant input)
        {
            // veryfiy mandatory fields
            if (input == null) return "input is null";
            Type type = input.GetType();
            foreach (var item in mandatoryNames)
            {
                var prop = type.GetProperty(item);
                if (prop?.GetValue(input, null) == null)
                {
                    return item + " is null";
                }
                if (prop?.PropertyType == typeof(string) && (string)prop?.GetValue(input) == "")
                {
                    return item + " is empty";
                }
            }

            // ContactInfos check
            if (input.contacts == null || input.contacts.Count == 0)
                return "contacts is null or empty";
            if (input.contacts[0].number == null || input.contacts[0].number == "")
                return "first contact number is null or empty";

            // DateofBirth check
            if (input.dateOfBirth is null)
                return "dateOfBirth is null";

            DateOnly birthDate = input.dateOfBirth.Value;
            DateOnly today = Globals.CampStart;

            int age = today.Year - birthDate.Year;
            if (today < birthDate.AddYears(age))
                age--;

            if (age < Globals.MinAge || age > Globals.MaxAge)
                return "age not in range";

            // selectedSlot check
            if (string.IsNullOrEmpty(input.selectedSlot) || !Globals.ValideSlots.Contains(input.selectedSlot))
            {
                return "selectedSlot is invalid";
            }

            // special slot date check
            if (input.selectedSlot == "Special")
            {
                if (input.startDate is null || input.endDate is null)
                {
                    return "startDate or endDate is null for Special slot";
                }
                if (input.startDate < Globals.CampStart || input.endDate > Globals.CampEnd || input.startDate > input.endDate)
                {
                    return "startDate or endDate is out of range for Special slot";
                }
            }

            // perms check
            if (string.IsNullOrEmpty(input.perms) || !Globals.ValidePerms.Contains(input.perms))
            {
                return "perms is invalid";
            }

            return "";
        }

        public int InsertParticipant(InParticipant input)
        {
            var tags = new List<string>();
            if (input.swimmer == true) tags.Add("swimmer");
            if (input.picturesAllowed == true) tags.Add("picturesAllowed");
            if (input.isHealthy == true) tags.Add("isHealthy");
            if (input.hasLiabilityInsurance == true) tags.Add("hasLiabilityInsurance");
            if (input.needsMedication == true) tags.Add("needsMeds");
            tags.Add(input.perms);

            var json = JsonSerializer.Serialize(input);
            var tagList = tags; // List<string>


            using var cmd = new NpgsqlCommand(
                "SELECT insert_participant(@data, @tags)", conn);

            cmd.Parameters.AddWithValue("data",
                NpgsqlTypes.NpgsqlDbType.Jsonb, json);

            cmd.Parameters.AddWithValue("tags",
                NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Text,
                tagList.ToArray());

            var personId = (int)cmd.ExecuteScalar();

            return personId;
        }
    }
}
