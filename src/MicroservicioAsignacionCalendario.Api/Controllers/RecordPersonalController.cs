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

        [HttpGet]
        [ProducesResponseType(typeof(List<RecordPersonalResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObtenerRecordsPersonales([FromBody] RecordPersonalFilterRequest filtros)
        {
            try
            {
                var result = await _service.ObtenerRecordsPersonalesAsync(filtros);
                return Ok(result);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new ApiError { Message = ex.Message });
            }
        }
    }
}
