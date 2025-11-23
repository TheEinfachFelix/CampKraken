using AusguckBackend.Models;
using BackendTest.Models;
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
        public static readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles,
            WriteIndented = true
        };
        public static T DeepCopy<T>(this T obj)
        {
            var json = JsonSerializer.Serialize(obj, options);
            return JsonSerializer.Deserialize<T>(json)!;
        }
        public static string SampleJson { get; } = "{\r\n    \"lastName\": \"sample\",\r\n   \"firstName\": \"a620a24c-3dd7-42c6-99f7-1c3932fdfaef\",\r\n   \"dateOfBirth\": \"2015-10-24\",\r\n   \"gender\": \"0\",\r\n   \"streetAndNumber\": \"dsfgsdfg\",\r\n   \"zipCode\": 34555,\r\n   \"city\": \"sdfgdfsg\",\r\n   \"contacts\": [\r\n      {\r\n        \"number\": \"345543\",\r\n         \"who\": \"dfgfg\"\r\n      }\r\n   ],\r\n   \"email\": \"dfsgdfsg@d.de\",\r\n   \"schoolType\": \"1\",\r\n   \"shirtSize\": \"7\",\r\n   \"hasLiabilityInsurance\": true,\r\n   \"perms\": \"Small group\",\r\n   \"swimmer\": true,\r\n   \"selectedSlot\": \"D2\",\r\n   \"nutrition\": [\r\n      \"1\"\r\n   ],\r\n   \"isHealthy\": true,\r\n   \"needsMedication\": false,\r\n   \"picturesAllowed\": false,\r\n   \"question1\": [\r\n      \"Item 1\"\r\n   ]\r\n}";
        public static string FullJson { get; } = "{\r\n    \"lastName\": \"full\",\r\n   \"firstName\": \"be7b6edf-6947-482f-822b-1e3e8040665c\",\r\n   \"dateOfBirth\": \"2015-10-22\",\r\n   \"gender\": \"0\",\r\n   \"streetAndNumber\": \"dfsaf\",\r\n   \"zipCode\": 23233,\r\n   \"city\": \"dafsdf\",\r\n   \"coverName\": \"sdfasdf\",\r\n   \"contacts\": [\r\n      {\r\n        \"number\": \"2233332\",\r\n         \"who\": \"dsfdsf\"\r\n      },\r\n      {\r\n        \"number\": \"3434343\",\r\n         \"who\": \"dsfadfdsfadf\"\r\n      }\r\n   ],\r\n   \"email\": \"dsaf@de.de\",\r\n   \"schoolType\": \"2\",\r\n   \"shirtSize\": \"7\",\r\n   \"hasLiabilityInsurance\": false,\r\n   \"perms\": \"supervised\",\r\n   \"swimmer\": true,\r\n   \"selectedSlot\": \"Special\",\r\n   \"start-date\": \"2026-07-20\",\r\n   \"end-date\": \"2026-08-01\",\r\n   \"daysInCamp\": 18,\r\n   \"userDiscountCode\": \"3434343\",\r\n   \"nutrition\": [\r\n      \"0\"\r\n   ],\r\n   \"intolerances\": \"dsaff\",\r\n   \"isHealthy\": true,\r\n   \"needsMedication\": true,\r\n   \"healthInfo\": \"dsafsdf\",\r\n   \"doctor\": \"dfsfdfsdf\",\r\n   \"healthInsuranceName\": \"saff\",\r\n   \"insuredBy\": \"dasfasdfasdf\",\r\n   \"picturesAllowed\": true,\r\n   \"question1\": [\r\n      \"Item 1\"\r\n   ],\r\n   \"specialInfos\": \"sadasdfadsf\"\r\n}";
        public static string MinJson { get; } = "{\r\n    \"lastName\": \"min\",\r\n   \"firstName\": \"b25705b5-9bbf-43cf-bbec-f65331197b91\",\r\n   \"dateOfBirth\": \"2015-10-29\",\r\n   \"gender\": \"0\",\r\n   \"zipCode\": 343434,\r\n   \"city\": \"dsfgdsfg\",\r\n   \"streetAndNumber\": \"fdfdsgdfg\",\r\n   \"contacts\": [\r\n      {\r\n        \"number\": \"2323223\"\r\n      }\r\n   ],\r\n   \"email\": \"dfdsg@de.dd\",\r\n   \"schoolType\": \"1\",\r\n   \"shirtSize\": \"8\",\r\n   \"hasLiabilityInsurance\": true,\r\n   \"perms\": \"alone\",\r\n   \"swimmer\": true,\r\n   \"selectedSlot\": \"D2\",\r\n   \"nutrition\": [\r\n      \"0\"\r\n   ],\r\n   \"isHealthy\": true,\r\n   \"needsMedication\": false,\r\n   \"picturesAllowed\": true,\r\n   \"question1\": [\r\n      \"Item 1\"\r\n   ]\r\n}";

        public static InParticipant SampleJsonDes { get; } = JsonSerializer.Deserialize<InParticipant>(SampleJson)!;
        public static InParticipant FullJsonDes { get; } = JsonSerializer.Deserialize<InParticipant>(FullJson)!;
        public static InParticipant MinJsonDes { get; } = JsonSerializer.Deserialize<InParticipant>(MinJson)!;
        
        public static person sampleperson = new person
        {
            lastName = "sample",
            firstName = "a620a24c-3dd7-42c6-99f7-1c3932fdfaef",
            dateOfBirth = new DateOnly(2015, 10, 24),
            genderId = 0,

            addresses = new List<address>
            {
                new address
                {
                    zipCode = 34555,
                    city = "sdfgdfsg",
                    streetAndNumber = "dsfgsdfg"
                }
            },

            contactInfos = new List<contactInfo>
            {
                new contactInfo
                {
                    contactInfoTypeId = 0, // Telefon
                    info = "345543",
                    details = "dfgfg"
                },
                new contactInfo
                {
                    contactInfoTypeId = 3, // E-Mail
                    info = "dfsgdfsg@d.de"
                }
            },

            participants = new List<participant>
            {
                new participant
                {
                    discountCodeId = 999,
                    shirtSizeId = 7,
                    selectedSlot = "D2",
                    person = null!, // EF füllt das nachträglich automatisch
                    participantsPrivate = new participantsPrivate
                    {
                        schoolTypeId = 1
                    },
                    tags = new List<tag>
                    {
                        new tag { tagId = 0, name = "swimmer" },
                        new tag { tagId = 2, name = "Small group" },
                        new tag { tagId = 4, name = "isHealthy" },
                        new tag { tagId = 5, name = "hasLiabilityInsurance" },
                    }
                }
            }
        };

        public static person fullPerson = new person
        {
            lastName = "full",
            firstName = "be7b6edf-6947-482f-822b-1e3e8040665c",
            dateOfBirth = new DateOnly(2015, 10, 22),
            genderId = 0,

            addresses = new List<address>
            {
                new address
                {
                    zipCode = 23233,
                    city = "dafsdf",
                    streetAndNumber = "dfsaf",
                    coverName = "sdfasdf"
                }
            },

            contactInfos = new List<contactInfo>
            {
                new contactInfo
                {
                    contactInfoTypeId = 0, // Telefon
                    info = "2233332",
                    details = "dsfdsf"
                },
                new contactInfo
                {
                    contactInfoTypeId = 0, // Telefon
                    info = "3434343",
                    details = "dsfadfdsfadf"
                },
                new contactInfo
                {
                    contactInfoTypeId = 3, // E-Mail
                    info = "dsaf@de.de"
                }
            },

            participants = new List<participant>
            {
                new participant
                {
                    discountCodeId = 999,
                    userDiscountCode = "3434343",
                    shirtSizeId = 7,
                    selectedSlot = "Special$2026-07-20$2026-08-01",
                    person = null!,
                    participantsPrivate = new participantsPrivate
                    {
                        schoolTypeId = 2,
                        intolerances = "dsaff",
                        healthInfo = "dsafsdf",
                        doctor = "dfsfdfsdf",
                        insuredBy = "dasfasdfasdf",
                        specialInfos = "sadasdfadsf",
                        healthInsuranceName = "saff"
                    },
                    tags = new List<tag>
                    {
                        new tag { tagId = 0, name = "swimmer" },
                        new tag { tagId = 3, name = "picturesAllowed" },
                        new tag { tagId = 4, name = "isHealthy" },
                        new tag { tagId = 6, name = "needsMeds" },
                        new tag { tagId = 7, name = "supervised"}
                    }
                }
            },
        };

        public static person minPerson = new person
        {
            lastName = "min",
            firstName = "b25705b5-9bbf-43cf-bbec-f65331197b91",
            dateOfBirth = new DateOnly(2015, 10, 29),
            genderId = 0,

            addresses = new List<address>
            {
                new address
                {
                    zipCode = 343434,
                    city = "dsfgdsfg",
                    streetAndNumber = "fdfdsgdfg"
                }
            },

            contactInfos = new List<contactInfo>
            {
                new contactInfo
                {
                    contactInfoTypeId = 0,
                    info = "2323223"
                },
                new contactInfo
                {
                    contactInfoTypeId = 3,
                    info = "dfdsg@de.dd"
                }
            },

            participants = new List<participant>
            {
                new participant
                {
                    discountCodeId = 999,
                    shirtSizeId = 8,
                    selectedSlot = "D2",
                    participantsPrivate = new participantsPrivate
                    {
                        schoolTypeId = 1
                    },
                    tags = new List<tag>
                    {
                        new tag { tagId = 0, name = "swimmer" },
                        new tag { tagId = 1, name = "alone" },
                        new tag { tagId = 3, name = "picturesAllowed" },
                        new tag { tagId = 4, name = "isHealthy" },
                        new tag { tagId = 5, name = "hasLiabilityInsurance" },
                    }
                }
            }
        };

    }
}
