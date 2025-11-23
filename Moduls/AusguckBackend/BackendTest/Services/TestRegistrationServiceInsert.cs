using AusguckBackend.Models;
using AusguckBackend.Services;
using BackendTest.Data;
using BackendTest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BackendTest.Services
{
    public class TestRegistrationServiceInsert
    {
        RegistrationService RegistrationService = new RegistrationService();
        private RumpfDbContext _context;

        [SetUp]
        public void Setup()
        { 
            _context = new RumpfDbContext();
        }

        [TearDown]
        public void TearDown()
        {
            CleanupTestData();
            _context.Dispose();
        }

        public void InsertTest(InParticipant data, person targetPerson)
        {
            var insertedId = RegistrationService.InsertParticipant(data);

            var personFromDb = _context.people
                .Include(p => p.addresses)
                .Include(p => p.contactInfos)
                .Include(p => p.participants)
                .ThenInclude(pa => pa.participantsPrivate)
                .Include(p => p.participants)
                .ThenInclude(pa => pa.tags)
                .First(p => p.personId == insertedId);

            person newPerson = personFromDb.DeepCopy();

            foreach (var item in newPerson.participants)
            {
                item.registrationDate = DateTime.MinValue;
                foreach (var tag in item.tags)
                {
                    tag.participants = [];
                }
            }

            var expectedJson = JsonSerializer.Serialize(targetPerson, SampleData.options);
            var actualJson = JsonSerializer.Serialize(newPerson, SampleData.options);

            List<string> problemIDs = ["personId", "addressId", "contactInfoId", "participantId"];

            foreach (var item in problemIDs)
            {
                var idPattern = new Regex("\"" + item + "\"\\s*:\\s*\\d+");
                var replacement = item + "RM";
                actualJson = idPattern.Replace(actualJson, replacement);
                expectedJson = idPattern.Replace(expectedJson, replacement);
            }

            Assert.That(actualJson, Is.EqualTo(expectedJson));
        }

        private void CleanupTestData()
        {
            var allSamplePersons = new List<person> { SampleData.fullPerson, SampleData.minPerson, SampleData.sampleperson };

            foreach (var sample in allSamplePersons)
            {
                var personsToRemove = _context.people
                    .Include(p => p.participants)
                        .ThenInclude(pa => pa.participantsPrivate)
                    .Include(p => p.participants)
                        .ThenInclude(pa => pa.tags)
                    .Include(p => p.participants)
                    .Include(p => p.addresses)
                    .Include(p => p.contactInfos)
                    .Where(p => p.firstName == sample.firstName && p.lastName == sample.lastName)
                    .ToList();

                foreach (var p in personsToRemove)
                {
                    foreach (var part in p.participants.ToList())
                    {
                        _context.participantsPrivates.RemoveRange(part.participantsPrivate);

                        part.tags.Clear(); // M2M relation
                        _context.participants.Remove(part);
                    }

                    _context.addresses.RemoveRange(p.addresses);
                    _context.contactInfos.RemoveRange(p.contactInfos);

                    _context.people.Remove(p);
                }
            }

            _context.SaveChanges();
        }

        [Test]
        public void FullInsert()
        {
            var data = SampleData.FullJsonDes.DeepCopy();
            InsertTest(data, SampleData.fullPerson);
        }

        [Test]
        public void MinInsert()
        {
            var data = SampleData.MinJsonDes.DeepCopy();
            InsertTest(data, SampleData.minPerson);
        }

        [Test]
        public void SampleInsert()
        {
            var data = SampleData.SampleJsonDes.DeepCopy();
            InsertTest(data, SampleData.sampleperson);
        }


    }
}
