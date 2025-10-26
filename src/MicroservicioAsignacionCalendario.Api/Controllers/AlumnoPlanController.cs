using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MicroservicioAsignacionCalendario.Application.CustomExceptions;
using MicroservicioAsignacionCalendario.Application.DTOs.AlumnoPlan;
using MicroservicioAsignacionCalendario.Application.Interfaces.AlumnoPlan;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicioAsignacionCalendario.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlumnoPlanController : ControllerBase
    {
        private readonly IAlumnoPlanService _service;

        public AlumnoPlanController(IAlumnoPlanService service)
        {
            _service = service;
        }

        [HttpPost("asignar")]
        [ProducesResponseType(typeof(AsignarPlanResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AsignarPlan([FromBody] AsignarPlanRequest request)
        {
            var result = await _service.AsignarPlanAsync(request);
            return CreatedAtAction(nameof(ObtenerPlanesPorAlumno),
                                   new { alumno_id = request.IdAlumno },
                                   result);
        }

        [HttpGet("{alumno_id:guid}")]
        [ProducesResponseType(typeof(IEnumerable<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObtenerPlanesPorAlumno([FromRoute] Guid alumno_id)
        {
            var result = await _service.ObtenerPlanesPorAlumnoAsync(alumno_id);
            return Ok(result);
        }
    }
}
