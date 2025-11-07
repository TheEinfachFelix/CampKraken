using AusguckBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BackendTest.Services
{
    internal static class SampleData
    {
        public static T DeepCopy<T>(this T obj)
        {
            var json = JsonSerializer.Serialize(obj);
            return JsonSerializer.Deserialize<T>(json)!;
        }
        public static string SampleJson { get; } = "{\r\n    \"lastName\": \"ertw\",\r\n   \"firstName\": \"dfsg\",\r\n   \"dateOfBirth\": \"2015-10-24\",\r\n   \"gender\": 0,\r\n   \"streetAndNumber\": \"dsfgsdfg\",\r\n   \"zipCode\": 34555,\r\n   \"city\": \"sdfgdfsg\",\r\n   \"contacts\": [\r\n      {\r\n        \"number\": \"345543\",\r\n         \"who\": \"dfgfg\"\r\n      }\r\n   ],\r\n   \"email\": \"dfsgdfsg@d.de\",\r\n   \"schoolType\": 1,\r\n   \"shirtSize\": 7,\r\n   \"hasLiabilityInsurance\": true,\r\n   \"perms\": \"Item 2\",\r\n   \"swimmer\": true,\r\n   \"selectedSlot\": \"D2\",\r\n   \"nutrition\": [\r\n      \"1\"\r\n   ],\r\n   \"isHealthy\": true,\r\n   \"needsMedication\": false,\r\n   \"picturesAllowed\": false,\r\n   \"question1\": [\r\n      \"Item 1\"\r\n   ]\r\n}";
        public static string FullJson { get; } = "{\r\n    \"lastName\": \"dsaf\",\r\n   \"firstName\": \"sdaf\",\r\n   \"dateOfBirth\": \"2015-10-22\",\r\n   \"gender\": 0,\r\n   \"streetAndNumber\": \"dfsaf\",\r\n   \"zipCode\": 23233,\r\n   \"city\": \"dafsdf\",\r\n   \"coverName\": \"sdfasdf\",\r\n   \"contacts\": [\r\n      {\r\n        \"number\": \"2233332\",\r\n         \"who\": \"dsfdsf\"\r\n      },\r\n      {\r\n        \"number\": \"3434343\",\r\n         \"who\": \"dsfadfdsfadf\"\r\n      }\r\n   ],\r\n   \"email\": \"dsaf@de.de\",\r\n   \"schoolType\": 2,\r\n   \"shirtSize\": 7,\r\n   \"hasLiabilityInsurance\": false,\r\n   \"perms\": \"Item 1\",\r\n   \"swimmer\": true,\r\n   \"selectedSlot\": \"Special\",\r\n   \"start-date\": \"2026-07-20\",\r\n   \"end-date\": \"2026-08-01\",\r\n   \"daysInCamp\": 18,\r\n   \"userDiscountCode\": \"3434343\",\r\n   \"nutrition\": [\r\n      \"0\"\r\n   ],\r\n   \"intolerances\": \"dsaff\",\r\n   \"isHealthy\": true,\r\n   \"needsMedication\": true,\r\n   \"healthInfo\": \"dsafsdf\",\r\n   \"doctor\": \"dfsfdfsdf\",\r\n   \"healthInsuranceName\": \"saff\",\r\n   \"insuredBy\": \"dasfasdfasdf\",\r\n   \"picturesAllowed\": true,\r\n   \"question1\": [\r\n      \"Item 1\"\r\n   ],\r\n   \"specialInfos\": \"sadasdfadsf\"\r\n}";
        public static string MinJson { get; } = "{\r\n    \"lastName\": \"fdsg\",\r\n   \"firstName\": \"dsfgdfsg\",\r\n   \"dateOfBirth\": \"2015-10-29\",\r\n   \"gender\": 0,\r\n   \"zipCode\": 343434,\r\n   \"city\": \"dsfgdsfg\",\r\n   \"streetAndNumber\": \"fdfdsgdfg\",\r\n   \"contacts\": [\r\n      {\r\n        \"number\": \"2323223\"\r\n      }\r\n   ],\r\n   \"email\": \"dfdsg@de.dd\",\r\n   \"schoolType\": 1,\r\n   \"shirtSize\": 8,\r\n   \"hasLiabilityInsurance\": true,\r\n   \"perms\": \"Item 1\",\r\n   \"swimmer\": true,\r\n   \"selectedSlot\": \"D2\",\r\n   \"nutrition\": [\r\n      \"0\"\r\n   ],\r\n   \"isHealthy\": true,\r\n   \"needsMedication\": false,\r\n   \"picturesAllowed\": true,\r\n   \"question1\": [\r\n      \"Item 1\"\r\n   ]\r\n}";

        public static InParticipant SampleJsonDes { get; } = JsonSerializer.Deserialize<InParticipant>(SampleJson)!;
        public static InParticipant FullJsonDes { get; } = JsonSerializer.Deserialize<InParticipant>(FullJson)!;
        public static InParticipant MinJsonDes { get; } = JsonSerializer.Deserialize<InParticipant>(MinJson)!;
        
        public static Person samplePerson = new Person
        {
            LastName = "ertw",
            FirstName = "dfsg",
            DateOfBirth = new DateOnly(2015, 10, 24),
            GenderId = 0,

            Addresses = new List<Address>
            {
                new Address
                {
                    ZipCode = 34555,
                    City = "sdfgdfsg",
                    StreetAndNumber = "dsfgsdfg"
                }
            },

            ContactInfos = new List<ContactInfo>
            {
                new ContactInfo
                {
                    ContactInfoTypeId = 0, // Telefon
                    Info = "345543",
                    Details = "dfgfg"
                },
                new ContactInfo
                {
                    ContactInfoTypeId = 3, // E-Mail
                    Info = "dfsgdfsg@d.de"
                }
            },

            Participants = new List<AusguckBackend.Models.Participant>
            {
                new AusguckBackend.Models.Participant
                {
                    DiscountCodeId = -1,
                    ShirtSizeId = 7,
                    SelectedSlot = "D2",
                    Person = null!, // EF füllt das nachträglich automatisch
                    ParticipantsPrivate = new ParticipantsPrivate
                    {
                        NutritionId = 1,
                        SchoolTypeId = 1
                    },
                    Tags = new List<Tag>
                    {
                        new Tag { TagId = 0, Name = "swimmer" },
                        new Tag { TagId = 4, Name = "isHealthy" },
                        new Tag { TagId = 5, Name = "hasLiabilityInsurance" }
                    }
                }
            }
        };

        public static Person fullPerson = new Person
        {
            LastName = "dsaf",
            FirstName = "sdaf",
            DateOfBirth = new DateOnly(2015, 10, 22),
            GenderId = 0,

            Addresses = new List<Address>
            {
                new Address
                {
                    ZipCode = 23233,
                    City = "dafsdf",
                    StreetAndNumber = "dfsaf",
                    CoverName = "sdfasdf"
                }
            },

            ContactInfos = new List<ContactInfo>
            {
                new ContactInfo
                {
                    ContactInfoTypeId = 0, // Telefon
                    Info = "2233332",
                    Details = "dsfdsf"
                },
                new ContactInfo
                {
                    ContactInfoTypeId = 0, // Telefon
                    Info = "3434343",
                    Details = "dsfadfdsfadf"
                },
                new ContactInfo
                {
                    ContactInfoTypeId = 3, // E-Mail
                    Info = "dsaf@de.de"
                }
            },

            Participants = new List<AusguckBackend.Models.Participant>
            {
                new AusguckBackend.Models.Participant
                {
                    DiscountCodeId = -1,
                    UserDiscountCode = "3434343",
                    ShirtSizeId = 7,
                    SelectedSlot = "Special$20.07.2026$01.08.2026",
                    Person = null!,
                    ParticipantsPrivate = new ParticipantsPrivate
                    {
                        NutritionId = 0,
                        SchoolTypeId = 2,
                        Intolerances = "dsaff",
                        HealthInfo = "dsafsdf",
                        Doctor = "dfsfdfsdf",
                        InsuredBy = "dasfasdfasdf",
                        SpecialInfos = "sadasdfadsf"
                    },
                    Tags = new List<Tag>
                    {
                        new Tag { TagId = 0, Name = "swimmer" },
                        new Tag { TagId = 3, Name = "picturesAllowed" },
                        new Tag { TagId = 4, Name = "isHealthy" },
                        new Tag { TagId = 6, Name = "needsMeds" }
                    }
                }
            },
        };

        public static Person minPerson = new Person
        {
            LastName = "fdsg",
            FirstName = "dsfgdfsg",
            DateOfBirth = new DateOnly(2015, 10, 29),
            GenderId = 0,

            Addresses = new List<Address>
            {
                new Address
                {
                    ZipCode = 343434,
                    City = "dsfgdsfg",
                    StreetAndNumber = "fdfdsgdfg"
                }
            },

            ContactInfos = new List<ContactInfo>
            {
                new ContactInfo
                {
                    ContactInfoTypeId = 0,
                    Info = "2323223"
                },
                new ContactInfo
                {
                    ContactInfoTypeId = 3,
                    Info = "dfdsg@de.dd"
                }
            },

            Participants = new List<AusguckBackend.Models.Participant>
            {
                new AusguckBackend.Models.Participant
                {
                    DiscountCodeId = -1,
                    ShirtSizeId = 8,
                    SelectedSlot = "D2",
                    ParticipantsPrivate = new ParticipantsPrivate
                    {
                        NutritionId = 0,
                        SchoolTypeId = 1
                    },
                    Tags = new List<Tag>
                    {
                        new Tag { TagId = 0, Name = "swimmer" },
                        new Tag { TagId = 3, Name = "picturesAllowed" },
                        new Tag { TagId = 4, Name = "isHealthy" },
                        new Tag { TagId = 5, Name = "hasLiabilityInsurance" }
                    }
                }
            }
        };

    }
}
