namespace MicroservicioAsignacionCalendario.Application.Interfaces.AlumnoPlan
{
    public interface IAlumnoPlanService
    {
        Task<object> AsignarPlanAsync(object request);
        Task<object> ObtenerPlanesPorAlumnoAsync(Guid alumnoId);
    }
}
