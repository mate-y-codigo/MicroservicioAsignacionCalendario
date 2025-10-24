using MicroservicioAsignacionCalendario.Application.CustomExceptions;
using MicroservicioAsignacionCalendario.Application.Interfaces.RecordPersonal;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicioAsignacionCalendario.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecordPersonalController : ControllerBase
    {
        private readonly IRecordPersonalService _service;

        public RecordPersonalController(IRecordPersonalService service)
        {
            _service = service;
        }

        [HttpGet("{alumno_id}")]
        [ProducesResponseType(typeof(IEnumerable<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObtenerRecordPersonal([FromRoute] Guid alumno_id)
        {
            var result = await _service.ObtenerRecordPersonalAsync(alumno_id);
            return Ok(result);
        }
    }
}
