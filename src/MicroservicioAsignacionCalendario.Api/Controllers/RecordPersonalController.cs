using MicroservicioAsignacionCalendario.Application.CustomExceptions;
using MicroservicioAsignacionCalendario.Application.DTOs.RecordPersonal;
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
        [ProducesResponseType(typeof(List<RecordPersonalResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        // TO DO: Agregar filtros por fecha, sesionEntrenamiento, ejercicio??
        public async Task<IActionResult> ObtenerRecordsPersonales([FromRoute] Guid alumno_id)
        {
            var result = await _service.ObtenerRecordsPersonalesAsync(alumno_id);
            return Ok(result);
        }
    }
}
