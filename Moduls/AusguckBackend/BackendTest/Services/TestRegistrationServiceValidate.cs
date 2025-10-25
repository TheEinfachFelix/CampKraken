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
    internal class TestRegistrationServiceValidate
    {
        [SetUp]
        public void Setup()
        {
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
            Assert.Pass();
        }
    }
}