using MicroservicioAsignacionCalendario.Application.CustomExceptions;
using MicroservicioAsignacionCalendario.Application.DTOs.EjercicioRegistro;
using MicroservicioAsignacionCalendario.Application.Interfaces.RegistroEjercicio;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicioAsignacionCalendario.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EjercicioRegistroController : ControllerBase
    {
        private readonly IEjercicioRegistroService _service;

        public EjercicioRegistroController(IEjercicioRegistroService service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(typeof(EjercicioRegistroResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegistrarEjercicio([FromBody] EjercicioRegistroRequest registro)
        {
            var result = await _service.RegistrarEjercicioAsync(registro);
            return Ok(result);
        }

        [HttpGet("{alumno_id}")]
        [ProducesResponseType(typeof(List<EjercicioRegistroResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObtenerRegistros([FromRoute] Guid alumno_id, [FromQuery] EjercicioRegistroFilterRequest filtros)
        {
            var result = await _service.ObtenerRegistrosAsync(alumno_id, filtros);
            return Ok(result);
        }
    }
}
