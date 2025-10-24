using MicroservicioAsignacionCalendario.Application.CustomExceptions;
using MicroservicioAsignacionCalendario.Application.Interfaces.RegistroEjercicio;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicioAsignacionCalendario.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistroEjercicioController : ControllerBase
    {
        private readonly IRegistroEjercicioService _service;

        public RegistroEjercicioController(IRegistroEjercicioService service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegistrarEjercicio([FromBody] object registro)
        {
            var result = await _service.RegistrarEjercicioAsync(registro);
            return Ok(result);
        }

        [HttpGet("{alumno_id}")]
        [ProducesResponseType(typeof(IEnumerable<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObtenerRegistros([FromRoute] Guid alumno_id)
        {
            var result = await _service.ObtenerRegistrosAsync(alumno_id);
            return Ok(result);
        }
    }
}
