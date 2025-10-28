using AusguckBackend.Models;
using System.Text.Json;

namespace AusguckBackend.Services
{
    public class RegistrationService : IRegistrationService
    {
        public static readonly List<string> mandatoryNames = ["lastName", "firstName", "dateOfBirth", "gender", "zipCode", "city", "streetAndNumber", "email", "schoolType", "shirtSize", "hasLiabilityInsurance", "perms", "swimmer", "selectedSlot", "nutrition", "isHealthy", "needsMedication", "picturesAllowed", "question1"];

        public Task ProcessIncomingDataAsync(Participant data)
        {
            throw new NotImplementedException();
        }

        public static string Verify(Participant input)
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

        public static Person MapToPerson(Participant input)
        {
            // 1. Person
            var person = new Person
            {
                LastName = input.lastName,
                FirstName = input.firstName,
                DateOfBirth = input.dateOfBirth.HasValue ? input.dateOfBirth.Value : null,
                GenderId = input.gender != null ? int.Parse(input.gender) : null,
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
                DiscountCodeId = -1,
                UserDiscountCode = input.userDiscountCode,
                ShirtSizeId = input.shirtSize != null ? int.Parse(input.shirtSize) : 0,
                SelectedSlot = input.selectedSlot,
                ParticipantsPrivate = new ParticipantsPrivate
                {
                    SchoolTypeId = input.schoolType != null ? int.Parse(input.schoolType) : 0,
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

            // 5. Tags // TODO: keinneune Tag und alone und small grupe fehlt
            if (input.swimmer == true) efParticipant.Tags.Add(new Tag { TagId = 0, Name = "swimmer" });
            if (input.picturesAllowed == true) efParticipant.Tags.Add(new Tag { TagId = 3, Name = "picturesAllowed" });
            if (input.isHealthy == true) efParticipant.Tags.Add(new Tag { TagId = 4, Name = "isHealthy" });
            if (input.hasLiabilityInsurance == true) efParticipant.Tags.Add(new Tag { TagId = 5, Name = "hasLiabilityInsurance" });
            if (input.needsMedication == true) efParticipant.Tags.Add(new Tag { TagId = 6, Name = "needsMeds" });

            person.Participants.Add(efParticipant);

            return person;
        }
    }
}

    //"contacts": ["number" ]
    
