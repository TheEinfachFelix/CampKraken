using AusguckBackend.Models;
using AusguckBackend.Services;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace BackendTest.Services
{
    internal class TestRegistrationServiceValidate
    {
        RegistrationService RegistrationService;
        [SetUp]
        public void Setup()
        {
            RegistrationService = new RegistrationService();
        }

        [Test]
        public void SampleValidation()
        {
            var output = RegistrationService.Verify(SampleData.SampleJsonDes);

            Assert.That(output, Is.EqualTo(""));
        }

        [Test]
        public void FullValidation()
        {
            var output = RegistrationService.Verify(SampleData.FullJsonDes);

            Assert.That(output, Is.EqualTo(""));
        }

        [Test]
        public void MinValidation()
        {
            var output = RegistrationService.Verify(SampleData.MinJsonDes);

            Assert.That(output, Is.EqualTo(""));
        }

        [Test]
        public void FailCases()
        {
            List<string> specials = ["startDate", "endDate", "contacts"];
            Type type = SampleData.FullJsonDes.GetType();
            foreach (var item in type.GetProperties())
            {
                var copy = SampleData.DeepCopy<InParticipant>(SampleData.FullJsonDes);
                // set to null
                item.SetValue(copy, null);
                var output = RegistrationService.Verify(copy);
                if (RegistrationService.mandatoryNames.Contains(item.Name) || specials.Contains(item.Name))
                    Assert.That(output, Is.Not.EqualTo(""), $"Failed null test for {item.Name}");
                else
                    Assert.That(output, Is.EqualTo(""), $"Failed null test for {item.Name}");
                // set to empty string if string
                if (item.PropertyType == typeof(string))
                {
                    copy = SampleData.DeepCopy<InParticipant>(SampleData.FullJsonDes);
                    item.SetValue(copy, "");
                    output = RegistrationService.Verify(copy);
                    if (RegistrationService.mandatoryNames.Contains(item.Name))
                        Assert.That(output, Is.Not.EqualTo(""), $"Failed empty string test for {item.Name}");
                    else
                        Assert.That(output, Is.EqualTo(""), $"Failed empty string test for {item.Name}");
                }
            }
        }

        [Test]
        public void TestBirthday()
        {
            List<DateOnly> dates = new List<DateOnly>
            {
                new DateOnly(2027,07,19),
                new DateOnly(2026,08,15),
                new DateOnly(2025,12,31),
                new DateOnly(2020,01,01),
                new DateOnly(2010,06,30),
                new DateOnly(2004,07,19),
                new DateOnly(1995,08,15)
            };
            List<bool> result = new List<bool>
            {
                false,
                false,
                false,
                true,
                true,
                false,
                false
            };
            for (int i = 0; i < dates.Count; i++)
            {
                var data = SampleData.DeepCopy<InParticipant>(SampleData.FullJsonDes);
                data.dateOfBirth = dates[i];
                var output = RegistrationService.Verify(data);
                if (result[i])
                {
                    Assert.That(output, Is.EqualTo(""), $"Failed birthday test for {dates[i]}");
                }
                else
                {
                    Assert.That(output, Is.Not.EqualTo(""), $"Failed birthday test for {dates[i]}");
                }
            }
        }

        [Test]
        public void TestSelectedSlot()
        {
            List<string> slots = new List<string>
            {
                "D1",
                "D2",
                "Special",
                "Invalid",
                "",
                null
            };
            List<bool> result = new List<bool>
            {
                true,
                true,
                true,
                false,
                false,
                false
            };
            for (int i = 0; i < slots.Count; i++)
            {
                var data = SampleData.DeepCopy<InParticipant>(SampleData.FullJsonDes);
                data.selectedSlot = slots[i];
                var output = RegistrationService.Verify(data);
                if (result[i])
                {
                    Assert.That(output, Is.EqualTo(""), $"Failed selectedSlot test for '{slots[i]}'");
                }
                else
                {
                    Assert.That(output, Is.Not.EqualTo(""), $"Failed selectedSlot test for '{slots[i]}'");
                }
            }
        }

        [Test]
        public void TestSelectedPerm()
        {
            List<string> slots = new List<string>
            {
                "alone",
                "Small group",
                "supervised",
                "Invalid",
                "",
                null
            };
            List<bool> result = new List<bool>
            {
                true,
                true,
                true,
                false,
                false,
                false
            };
            for (int i = 0; i < slots.Count; i++)
            {
                var data = SampleData.DeepCopy<InParticipant>(SampleData.FullJsonDes);
                data.perms = slots[i];
                var output = RegistrationService.Verify(data);
                if (result[i])
                {
                    Assert.That(output, Is.EqualTo(""), $"Failed selectedSlot test for '{slots[i]}'");
                }
                else
                {
                    Assert.That(output, Is.Not.EqualTo(""), $"Failed selectedSlot test for '{slots[i]}'");
                }
            }
        }

        [Test]
        public void TestSpecialSlot()
        {
            List<DateOnly> StartDates = new List<DateOnly>
            {
                new DateOnly(2026,07,19),
                new DateOnly(2026,07,25),
                new DateOnly(2026,08,01),
                new DateOnly(2026,08,10)
            };
            List<DateOnly> EndDates = new List<DateOnly>
            {
                new DateOnly(2026,08,15),
                new DateOnly(2026,08,10),
                new DateOnly(2026,08,15),
                new DateOnly(2026,08,20)
            };
            List<bool> result = new List<bool>
            {
                true,
                true,
                true,
                false
            };
            for (int i = 0; i < StartDates.Count; i++)
            {
                var data = SampleData.DeepCopy<InParticipant>(SampleData.FullJsonDes);
                data.selectedSlot = "Special";
                data.startDate = StartDates[i];
                data.endDate = EndDates[i];
                var output = RegistrationService.Verify(data);
                if (result[i])
                {
                    Assert.That(output, Is.EqualTo(""), $"Failed special slot test for '{StartDates[i]} - {EndDates[i]}'");
                }
                else
                {
                    Assert.That(output, Is.Not.EqualTo(""), $"Failed special slot test for '{StartDates[i]} - {EndDates[i]}'");
                }
            }
        }

        [Test]
        public void TestContactInfos()
        {
            // Test null contacts
            {
                var data = SampleData.DeepCopy<InParticipant>(SampleData.FullJsonDes);
                data.contacts = null;
                var output = RegistrationService.Verify(data);
                Assert.That(output, Is.Not.EqualTo(""), "Failed contacts null test");
            }
            // Test empty contacts
            {
                var data = SampleData.DeepCopy<InParticipant>(SampleData.FullJsonDes);
                data.contacts = [];
                var output = RegistrationService.Verify(data);
                Assert.That(output, Is.Not.EqualTo(""), "Failed contacts empty test");
            }
            // Test first contact number null
            {
                var data = SampleData.DeepCopy<InParticipant>(SampleData.FullJsonDes);
                data.contacts = [];
                data.contacts.Add(new Contact());
                data.contacts[0].number = null;
                var output = RegistrationService.Verify(data);
                Assert.That(output, Is.Not.EqualTo(""), "Failed first contact number null test");
            }
            // Test first contact number empty
            {
                var data = SampleData.DeepCopy<InParticipant>(SampleData.FullJsonDes);
                data.contacts = [];
                data.contacts.Add(new Contact());
                data.contacts[0].number = "";
                var output = RegistrationService.Verify(data);
                Assert.That(output, Is.Not.EqualTo(""), "Failed first contact number empty test");
            }
            // Test first contact number valid
            {
                var data = SampleData.DeepCopy<InParticipant>(SampleData.FullJsonDes);
                data.contacts = [];
                data.contacts.Add(new Contact());
                data.contacts[0].number = "1234567890";
                var output = RegistrationService.Verify(data);
                Assert.That(output, Is.EqualTo(""), "Failed first contact number valid test");
            }
        }
    }
}