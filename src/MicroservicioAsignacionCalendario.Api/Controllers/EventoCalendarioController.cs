using MicroservicioAsignacionCalendario.Application.CustomExceptions;
using MicroservicioAsignacionCalendario.Application.Interfaces.EventoCalendario;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicioAsignacionCalendario.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoCalendarioController : ControllerBase
    {
        private readonly IEventoCalendarioService _service;

        public EventoCalendarioController(IEventoCalendarioService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObtenerEventos(
            [FromQuery] DateTime? desde,
            [FromQuery] DateTime? hasta,
            [FromQuery] string? alumnoId)
        {
            var result = await _service.ObtenerEventosAsync(desde, hasta, alumnoId);
            return Ok(result);
        }
    }
}
