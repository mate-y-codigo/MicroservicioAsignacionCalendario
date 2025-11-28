using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroservicioAsignacionCalendario.Application.Interfaces.Query;
using MicroservicioAsignacionCalendario.Application.DTOs.metricas;



namespace MicroservicioAsignacionCalendario.Api.Controllers
{
    [ApiController]
    [Route("api/metricas")]
    public class MetricasController : ControllerBase
    {
        private readonly IMetricsService _metricsService;

        public MetricasController(IMetricsService metricsService)
        {
            _metricsService = metricsService;
        }

        [Authorize(Roles = "Entrenador")]
        [HttpGet("grupales")]
        public async Task<ActionResult<MetricaResponseDto>> GetGrupales(
            [FromQuery] Guid idEntrenador,
            [FromQuery] DateTime desde,
            [FromQuery] DateTime hasta)
        {
            var result = await _metricsService.GetMetricasGrupalesAsync(idEntrenador, desde, hasta);
            return Ok(result);
        }

        [Authorize(Roles = "Entrenador")]
        [HttpGet("alumno/{idAlumno}")]
        public async Task<ActionResult<MetricaResponseDto>> GetAlumno(
            Guid idAlumno,
            [FromQuery] DateTime desde,
            [FromQuery] DateTime hasta)
        {
            var result = await _metricsService.GetMetricasAlumnoAsync(idAlumno, desde, hasta);
            return Ok(result);
        }
    }
}
