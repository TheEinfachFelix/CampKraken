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

            var innerMessage = exception?.InnerException?.Message;
            var details = exception?.Message;

            // Für EF-Fehler: gib auch die InnerException mit aus
            var errorResponse = new
            {
                error = exception?.GetType().Name,
                message = details,
                inner = innerMessage
            };

            if (exception is ArgumentException)
                return BadRequest(errorResponse);

            if (exception is InvalidOperationException)
                return Conflict(errorResponse);

            if (exception is Microsoft.EntityFrameworkCore.DbUpdateException)
                return StatusCode(500, new
                {
                    error = "Database update failed",
                    message = details,
                    inner = innerMessage
                });

            // Fallback
            return StatusCode(500, new
            {
                error = "Unexpected server error",
                message = details,
                inner = innerMessage
            });
        }
    }
}
