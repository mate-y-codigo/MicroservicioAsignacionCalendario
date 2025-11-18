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

        [HttpGet]
        [ProducesResponseType(typeof(List<EjercicioRegistroResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObtenerRegistros([FromQuery] EjercicioRegistroFilterRequest filtros)
        {
            var result = await _service.ObtenerRegistrosAsync(filtros);
            return Ok(result);
        }
    }
}
