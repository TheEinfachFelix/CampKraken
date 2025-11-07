using AusguckBackend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTest.Services
{
    public class TestRegistrationServiceInsert
    {
        RegistrationService RegistrationService;
        [SetUp]
        public void Setup()
        {
            RegistrationService = new RegistrationService();
        }
        [Test]
        public void FullInsert()
        {
            var data = SampleData.FullJsonDes.DeepCopy();
            RegistrationService.InsertParticipant(data);
            Assert.Pass();
        }
    }
}
