using MicroservicioAsignacionCalendario.Application.CustomExceptions;
using MicroservicioAsignacionCalendario.Application.DTOs.AlumnoPlan;
using MicroservicioAsignacionCalendario.Application.Interfaces.AlumnoPlan;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicioAsignacionCalendario.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlumnoPlanController : Controller
    {
        private readonly IAlumnoPlanService _service;

        public AlumnoPlanController(IAlumnoPlanService alumnoPlanService)
        {
            _service = alumnoPlanService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AlumnoPlanResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AsignarPlan([FromBody] AlumnoPlanRequest request)
        {
            var result = await _service.AsignarPlanAsync(request);
            return Ok(result);
        }

        [HttpGet("{alumno_id}")]
        [ProducesResponseType(typeof(List<AlumnoPlanResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObtenerPlanesPorAlumno([FromRoute] Guid alumno_id)
        {
            var result = await _service.ObtenerPlanesPorAlumnoAsync(alumno_id);
            return Ok(result);
        }
    }
}
