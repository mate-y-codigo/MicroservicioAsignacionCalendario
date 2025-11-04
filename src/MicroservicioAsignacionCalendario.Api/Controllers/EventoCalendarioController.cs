using Application.DTOs.EventoCalendario;
using Application.Interfaces.EventoCalendario;
using MicroservicioAsignacionCalendario.Application.CustomExceptions;
using MicroservicioAsignacionCalendario.Application.DTOs.EventoCalendario;
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
        [ProducesResponseType(typeof(List<EventoCalendarioResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObtenerEventos([FromQuery] EventoCalendarioFilterRequest filtros)
        {
            var result = await _service.ObtenerEventosAsync(filtros);
            return Ok(result);
        }
    }
}
