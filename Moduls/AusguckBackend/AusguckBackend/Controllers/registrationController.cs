using AusguckBackend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AusguckBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class registrationController : ControllerBase // Fix: inherit from ControllerBase to access Ok()
    {
        private readonly IRegistrationService _service;

        public registrationController(IRegistrationService service)
        {
            _service = service;
        }

        [HttpGet(Name = "PostRegistration")]
        public async Task<IActionResult> Post([FromBody] Services.Participant data)
        {
            await _service.ProcessIncomingDataAsync(data);
            return Ok();
        }
    }
}
