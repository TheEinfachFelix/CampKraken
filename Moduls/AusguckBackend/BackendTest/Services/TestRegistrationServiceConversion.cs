using AusguckBackend.Services;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace BackendTest.Services
{
    internal class TestRegistrationServiceConversion
    {
        RegistrationService RegistrationService;
        [SetUp]
        public void Setup()
        {
            RegistrationService = new RegistrationService();
        }

        [Test]
        public void SampleConversion()
        {
            var output = RegistrationService.MapToPerson(SampleData.SampleJsonDes);

            var expectedJson = JsonSerializer.Serialize(SampleData.samplePerson);
            var actualJson = JsonSerializer.Serialize(output);

            Assert.That(actualJson, Is.EqualTo(expectedJson));
        }

        [Test]
        public void FullConversion()
        {
            var output = RegistrationService.MapToPerson(SampleData.FullJsonDes);

            var expectedJson = JsonSerializer.Serialize(SampleData.fullPerson);
            var actualJson = JsonSerializer.Serialize(output);

            Assert.That(actualJson, Is.EqualTo(expectedJson));
        }

        [Test]
        public void MinConversion()
        {
            var output = RegistrationService.MapToPerson(SampleData.MinJsonDes);

            var expectedJson = JsonSerializer.Serialize(SampleData.minPerson);
            var actualJson = JsonSerializer.Serialize(output);

            Assert.That(actualJson, Is.EqualTo(expectedJson));
        }
    }
}