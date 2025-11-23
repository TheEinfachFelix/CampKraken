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
            //var personsToDelete = _context.people
            //    .Include(p => p.participants)
            //    .ThenInclude(pa => pa.participantsPrivate)
            //    .Include(p => p.participants)
            //    .ThenInclude(pa => pa.tags)
            //    .Include(p => p.contactInfos)
            //    .Include(p => p.addresses)
            //    .Where(p => p.lastName == SampleData.fullPerson.lastName &&
            //                p.firstName == SampleData.fullPerson.firstName)
            //    .ToList();

            //foreach (var person in personsToDelete)
            //{
            //    _context.participants.RemoveRange(person.participants);
            //    _context.contactInfos.RemoveRange(person.contactInfos);
            //    _context.addresses.RemoveRange(person.addresses);
            //    _context.people.Remove(person);
            //}

            //_context.SaveChanges();

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
