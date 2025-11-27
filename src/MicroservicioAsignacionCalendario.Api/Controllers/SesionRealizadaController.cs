using MicroservicioAsignacionCalendario.Api.Helpers;
using MicroservicioAsignacionCalendario.Application.CustomExceptions;
using MicroservicioAsignacionCalendario.Application.DTOs.SesionRealizada;
using MicroservicioAsignacionCalendario.Application.Interfaces.SesionRealizada;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(List<SesionRealizadaResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObtenerSesionesRealizadas([FromHeader(Name = "Authorization")] string authorizationHeader, [FromQuery] SesionRealizadaFilterRequest filtros)
        {
            var token = Request.ExtraerToken();
            if (string.IsNullOrEmpty(token))
                return Unauthorized(new ApiError { Message = "Token de autorización ausente o mal formado." });

            var entrenadorIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(entrenadorIdString) || !Guid.TryParse(entrenadorIdString, out var entrenadorId))
                return Unauthorized(new ApiError { Message = "Token inválido: no contiene ID de usuario válido." });

            Console.WriteLine($"Entrenador ID desde token: {entrenadorId}");
            filtros.IdEntrenador = entrenadorId;
            try
            {
                var result = await _service.ObtenerSesionesRealizadas(filtros);
                return Ok(result);
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
