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
            InsertParticipant(data);

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

        public Person MapToPerson(InParticipant data)
        {
            return new Person
            {
                LastName = data.lastName,
                FirstName = data.firstName,
                DateOfBirth = data.dateOfBirth.Value,
            };
        }
        public void InsertParticipant(InParticipant input)
        {
            // 1️⃣ Person anlegen
            var person = new Person
            {
                LastName = input.lastName,
                FirstName = input.firstName,
                DateOfBirth = input.dateOfBirth.Value,
                GenderId = input.gender ?? null,
                Addresses = new List<Address>(),
                ContactInfos = new List<ContactInfo>()
            };

            person.Addresses.Add(new Address
            {
                City = input.city,
                StreetAndNumber = input.streetAndNumber,
                ZipCode = input.zipCode,
                CoverName = input.coverName
            });

            foreach (var c in input.contacts)
            {
                person.ContactInfos.Add(new ContactInfo
                {
                    ContactInfoTypeId = 0,
                    Info = c.number ?? string.Empty,
                    Details = c.who
                });
            }

            if (!string.IsNullOrEmpty(input.email))
            {
                person.ContactInfos.Add(new ContactInfo
                {
                    ContactInfoTypeId = 3,
                    Info = input.email
                });
            }

            // 2️⃣ Teilnehmer erzeugen
            var participant = new Participant
            {
                DiscountCodeId = Globals.NotCheckedDiscountCodeId,
                UserDiscountCode = input.userDiscountCode,
                ShirtSizeId = input.shirtSize ?? 1,
                SelectedSlot = input.selectedSlot,
                ParticipantsPrivate = new ParticipantsPrivate
                {
                    SchoolTypeId = input.schoolType ?? 1,
                    NutritionId = (input.nutrition != null && input.nutrition.Count > 0)
                        ? int.Parse(input.nutrition[0])
                        : 1,
                    HealthInfo = input.healthInfo,
                    Doctor = input.doctor,
                    InsuredBy = input.insuredBy,
                    Intolerances = input.intolerances,
                    SpecialInfos = input.specialInfos
                }
            };

            // 3️⃣ Speichern (Person + Participant)
            _context.People.Add(person);
            _context.SaveChanges();

            participant.PersonId = person.PersonId;
            _context.Participants.Add(participant);
            _context.SaveChanges(); // Participant existiert jetzt mit ID

            // 🧩 WICHTIG: Reload Participant, damit EF ihn tracked!
            var trackedParticipant = _context.Participants
                .Include(p => p.Tags)
                .First(p => p.ParticipantId == participant.ParticipantId);

            // 4️⃣ Tags laden
            var tagNames = new List<string>();
            if (input.swimmer == true) tagNames.Add("swimmer");
            if (input.picturesAllowed == true) tagNames.Add("picturesAllowed");
            if (input.isHealthy == true) tagNames.Add("isHealthy");
            if (input.hasLiabilityInsurance == true) tagNames.Add("hasLiabilityInsurance");
            if (input.needsMedication == true) tagNames.Add("needsMeds");

            var existingTags = _context.Tags
                .Where(t => tagNames.Contains(t.Name))
                .ToList();

            // 5️⃣ Tags zuordnen
            foreach (var tag in existingTags)
            {
                trackedParticipant.Tags.Add(tag);
            }

            _context.SaveChanges();
        }
    }
}
