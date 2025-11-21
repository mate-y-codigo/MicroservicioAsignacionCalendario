using Application.DTOs.AlumnoPlan;
using MicroservicioAsignacionCalendario.Api.Helpers;
using MicroservicioAsignacionCalendario.Application.CustomExceptions;
using MicroservicioAsignacionCalendario.Application.DTOs.AlumnoPlan;
using MicroservicioAsignacionCalendario.Application.Interfaces.AlumnoPlan;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
        public async Task<IActionResult> AsignarPlan([FromHeader(Name = "Authorization")] string authorizationHeader, [FromBody] AlumnoPlanRequest request)
        {
            var token = Request.ExtraerToken();
            if (string.IsNullOrEmpty(token))
                return Unauthorized(new ApiError { Message = "Token de autorización ausente o mal formado." });

            try
            {
                var result = await _service.AsignarPlanAsync(token, request);
                return new JsonResult(result) { StatusCode = 201 };
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new ApiError { Message = ex.Message });
            }
            catch (ConflictException ex)
            {
                return Conflict(new ApiError { Message = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, new ApiError { Message = "Error interno del servidor." });
            }
        }

        [HttpGet("{alumno_id}")]
        [ProducesResponseType(typeof(List<AlumnoPlanResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObtenerPlanesPorAlumno([FromRoute] Guid alumno_id)
        {
            var result = await _service.ObtenerPlanesPorAlumnoAsync(alumno_id);
            return Ok(result);
        }

        [HttpGet("check/{planEntrenamiento_id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PlanEntrenamientoAsignado([FromRoute] Guid planEntrenamiento_id)
        {
            try
            {
                var result = await _service.PlanEntrenamientoAsignado(planEntrenamiento_id);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiError { Message = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, new ApiError { Message = "Error interno del servidor." });
            }
        }
    }
}
