using MicroservicioAsignacionCalendario.Application.CustomExceptions;
using MicroservicioAsignacionCalendario.Application.DTOs.Metricas;
using MicroservicioAsignacionCalendario.Application.Interfaces.Metricas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MicroservicioAsignacionCalendario.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetricasController : ControllerBase
    {
        private readonly IMetricasService _metricasService;

        public MetricasController(IMetricasService metricasService)
        {
            _metricasService = metricasService;
        }

        // GET: api/metricas/grupales?desde=2025-01-01&hasta=2025-01-31
        [Authorize]
        [HttpGet("grupales")]
        [ProducesResponseType(typeof(MetricasGrupalesResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObtenerMetricasGrupales(
            [FromQuery] DateTime desde,
            [FromQuery] DateTime hasta)
        {
            try
            {
                if (desde == default || hasta == default || desde > hasta)
                    return BadRequest(new ApiError { Message = "Rango de fechas inválido." });

                // Tomamos el Id del entrenador desde el token
                var claim = User.Claims.FirstOrDefault(c =>
                    c.Type == "id" || c.Type == ClaimTypes.NameIdentifier);

                if (claim == null || !Guid.TryParse(claim.Value, out var idEntrenador))
                    return Unauthorized(new ApiError { Message = "No se pudo obtener el Id del entrenador del token." });

                var result = await _metricasService.ObtenerMetricasGrupalesAsync(
                    idEntrenador, desde, hasta);

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
