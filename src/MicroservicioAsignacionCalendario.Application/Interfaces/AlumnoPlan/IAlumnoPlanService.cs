using MicroservicioAsignacionCalendario.Application.DTOs.AlumnoPlan;

namespace MicroservicioAsignacionCalendario.Application.Interfaces.AlumnoPlan
{
    public interface IAlumnoPlanService
    {
        Task<AlumnoPlanResponse> AsignarPlanAsync(AlumnoPlanRequest request);
        Task<List<AlumnoPlanResponse>> ObtenerPlanesPorAlumnoAsync(Guid alumnoId);
    }
}
