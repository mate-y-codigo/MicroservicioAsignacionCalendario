using MicroservicioAsignacionCalendario.Application.CustomExceptions;
using MicroservicioAsignacionCalendario.Application.DTOs.SesionRealizada;
using MicroservicioAsignacionCalendario.Application.Interfaces.SesionRealizada;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicioAsignacionCalendario.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SesionRealizadaController : Controller
    {
        private readonly ISesionRealizadaService _service;
        public SesionRealizadaController(ISesionRealizadaService service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(typeof(SesionRealizadaResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RegistrarSesionRealizada(SesionRealizadaRequest req)
        {
            try
            {
                var result = await _service.InsertarSesionRealizada(req);
                return new JsonResult(result) { StatusCode = 201 };
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new ApiError { Message = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, new ApiError { Message = "Error interno del servidor." });
            }
        }
    }
}
