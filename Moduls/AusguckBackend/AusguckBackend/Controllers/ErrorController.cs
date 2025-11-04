using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace AusguckBackend.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        public IActionResult HandleError()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;

            if (exception is ArgumentException)
                return BadRequest(new { error = exception.Message });
            if (exception is InvalidOperationException)
                return Conflict(new { error = exception.Message });

            return StatusCode(500, new { error = "Unexpected server error", details = exception?.Message });
        }
    }
}
