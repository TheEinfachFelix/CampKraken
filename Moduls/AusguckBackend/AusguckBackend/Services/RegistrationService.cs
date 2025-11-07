using AusguckBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace AusguckBackend.Services
{
    public class RegistrationService : IRegistrationService
    {
        public static readonly List<string> mandatoryNames = ["lastName", "firstName", "dateOfBirth", "gender", "zipCode", "city", "streetAndNumber", "email", "schoolType", "shirtSize", "hasLiabilityInsurance", "perms", "swimmer", "selectedSlot", "nutrition", "isHealthy", "needsMedication", "picturesAllowed", "question1"];
        RumpfContext _context = new RumpfContext();

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
            Person person = MapToPerson(data);

            InsertParticipant(person);

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


            return "";
        }

        public Person MapToPerson(InParticipant input)
        {
            // 1. Person
            var person = new Person
            {
                LastName = input.lastName,
                FirstName = input.firstName,
                DateOfBirth = input.dateOfBirth.HasValue ? input.dateOfBirth.Value : null,
                GenderId = input.gender != null ? input.gender : null,
                Addresses = new List<Address>(),
                ContactInfos = new List<ContactInfo>(),
                Participants = new List<Models.Participant>(),
                Days = new List<Day>()
            };

            // 2. Address
            person.Addresses.Add(new Address
            {
                City = input.city,
                StreetAndNumber = input.streetAndNumber,
                ZipCode = input.zipCode,
                CoverName = input.coverName,
            });

            // 3. ContactInfos
            if (input.contacts != null)
            {
                foreach (var c in input.contacts)
                {
                    person.ContactInfos.Add(new ContactInfo
                    {
                        ContactInfoTypeId = 0, // Telefon
                        Info = c.number ?? string.Empty,
                        Details = c.who,
                    });
                }
            }

            if (!string.IsNullOrEmpty(input.email))
            {
                person.ContactInfos.Add(new ContactInfo
                {
                    ContactInfoTypeId = 3, // E-Mail
                    Info = input.email,
                });
            }

            // 4. Participant (EF)
            var efParticipant = new Models.Participant
            {
                DiscountCodeId = Globals.NotCheckedDiscountCodeId,
                UserDiscountCode = input.userDiscountCode,
                ShirtSizeId = (int)(input.shirtSize != null ? input.shirtSize : 0),
                SelectedSlot = input.selectedSlot,
                ParticipantsPrivate = new ParticipantsPrivate
                {
                    SchoolTypeId = (int)(input.schoolType != null ? input.schoolType : 0),
                    NutritionId = input.nutrition != null && input.nutrition.Count > 0 ? int.Parse(input.nutrition[0]) : 0,
                    HealthInfo = input.healthInfo,
                    Doctor = input.doctor,
                    InsuredBy = input.insuredBy,
                    Intolerances = input.intolerances,
                    SpecialInfos = input.specialInfos
                },
                Tags = new List<Tag>()
            };
            if (input.selectedSlot == "Special") { efParticipant.SelectedSlot += "$" + input.startDate.ToString() + "$" + input.endDate.ToString(); }

            // Tags zuordnen
            var tagNames = new List<string>();
            if (input.swimmer == true) tagNames.Add("swimmer");
            if (input.picturesAllowed == true) tagNames.Add("picturesAllowed");
            if (input.isHealthy == true) tagNames.Add("isHealthy");
            if (input.hasLiabilityInsurance == true) tagNames.Add("hasLiabilityInsurance");
            if (input.needsMedication == true) tagNames.Add("needsMeds");

            var existingTags = _context.Tags
                .Where(t => tagNames.Contains(t.Name))
                .ToList();

            foreach (var tag in existingTags)
            {
                efParticipant.Tags.Add(tag);
            }

            person.Participants.Add(efParticipant);

            return person;
        }

        public void InsertParticipant(Person person)
        {

            _context.People.Add(person);
            _context.SaveChanges();
        }
    }
}
