using Application.DTOs.AlumnoPlan;
using MicroservicioAsignacionCalendario.Application.DTOs.AlumnoPlan;

namespace MicroservicioAsignacionCalendario.Application.Interfaces.AlumnoPlan
{
    public interface IAlumnoPlanService
    {
        Task<AlumnoPlanResponse> AsignarPlanAsync(string Token, AlumnoPlanRequest request);
        Task<List<AlumnoPlanResponse>> ObtenerPlanesPorAlumnoAsync(Guid alumnoId);
    }
}
