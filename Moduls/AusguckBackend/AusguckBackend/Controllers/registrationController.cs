using AusguckBackend.Models;
using AusguckBackend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AusguckBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class registrationController : ControllerBase // Fix: inherit from ControllerBase to access Ok()
    {
        private readonly RegistrationService _service;

        public registrationController()
        {
            _service = new();
        }

        [HttpPost(Name = "Registration")]
        public async Task<IActionResult> Post([FromBody] InParticipant data)
        {
            await new RegistrationService().ProcessIncomingDataAsync(data);
            return Ok();
        }
    }
}
