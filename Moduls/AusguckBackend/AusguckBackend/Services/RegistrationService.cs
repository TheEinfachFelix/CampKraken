using AusguckBackend.Models;
using System.Text.Json;

namespace AusguckBackend.Services
{
    public class RegistrationService : IRegistrationService
    {
        public Task ProcessIncomingDataAsync(Participant data)
        {
            throw new NotImplementedException();
        }

        public static Person MapToPerson(Participant input)
        {
            // 1. Person
            var person = new Person
            {
                LastName = input.LastName,
                FirstName = input.FirstName,
                DateOfBirth = input.DateOfBirth.HasValue ? DateOnly.FromDateTime(input.DateOfBirth.Value) : null,
                GenderId = input.Gender != null ? int.Parse(input.Gender) : null,
                Addresses = new List<Address>(),
                ContactInfos = new List<ContactInfo>(),
                Participants = new List<Models.Participant>(),
                Days = new List<Day>()
            };

            // 2. Address
            person.Addresses.Add(new Address
            {
                City = input.City,
                StreetAndNumber = input.StreetAndNumber,
                ZipCode = input.ZipCode,
                CoverName = input.CoverName,
                Person = person
            });

            // 3. ContactInfos
            if (input.Contacts != null)
            {
                foreach (var c in input.Contacts)
                {
                    person.ContactInfos.Add(new ContactInfo
                    {
                        ContactInfoTypeId = 0, // Telefon
                        Info = c.Number ?? string.Empty,
                        Details = c.Who,
                        Person = person
                    });
                }
            }

            if (!string.IsNullOrEmpty(input.Email))
            {
                person.ContactInfos.Add(new ContactInfo
                {
                    ContactInfoTypeId = 3, // E-Mail
                    Info = input.Email,
                    Person = person
                });
            }

            // 4. Participant (EF)
            var efParticipant = new Models.Participant
            {
                Person = person,
                DiscountCodeId = -1,
                UserDiscountCode = input.UserDiscountCode,
                ShirtSizeId = input.ShirtSize != null ? int.Parse(input.ShirtSize) : 0,
                SelectedSlot = input.SelectedSlot,
                RegistrationDate = DateTime.Now,
                ParticipantsPrivate = new ParticipantsPrivate
                {
                    Participant = null!, // wird nachträglich gesetzt
                    SchoolTypeId = input.SchoolType != null ? int.Parse(input.SchoolType) : 0,
                    NutritionId = input.Nutrition != null && input.Nutrition.Count > 0 ? int.Parse(input.Nutrition[0]) : 0,
                    HealthInfo = input.HealthInfo,
                    Doctor = input.Doctor,
                    InsuredBy = input.InsuredBy,
                    Intolerances = input.Intolerances,
                    SpecialInfos = input.SpecialInfos
                },
                Tags = new List<Tag>()
            };

            // Set Participant reference in ParticipantsPrivate
            efParticipant.ParticipantsPrivate.Participant = efParticipant;

            // 5. Tags
            if (input.Swimmer == true) efParticipant.Tags.Add(new Tag { TagId = 0, Name = "swimmer" });
            if (input.PicturesAllowed == true) efParticipant.Tags.Add(new Tag { TagId = 3, Name = "picturesAllowed" });
            if (input.IsHealthy == true) efParticipant.Tags.Add(new Tag { TagId = 4, Name = "isHealthy" });
            if (input.HasLiabilityInsurance == true) efParticipant.Tags.Add(new Tag { TagId = 5, Name = "hasLiabilityInsurance" });
            if (input.NeedsMedication == true) efParticipant.Tags.Add(new Tag { TagId = 6, Name = "needsMeds" });

            // 6. Days
            if (input.StartDate.HasValue && input.EndDate.HasValue)
            {
                var start = DateOnly.FromDateTime(input.StartDate.Value);
                var end = DateOnly.FromDateTime(input.EndDate.Value);
                for (var d = start; d <= end; d = d.AddDays(1))
                {
                    efParticipant.Person.Days.Add(new Day { Date = d, People = new List<Person> { person } });
                }
            }

            person.Participants.Add(efParticipant);

            return person;
        }
    }
}
