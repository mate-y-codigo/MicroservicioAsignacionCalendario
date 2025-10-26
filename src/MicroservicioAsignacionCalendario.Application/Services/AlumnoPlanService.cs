using MicroservicioAsignacionCalendario.Application.DTOs.AlumnoPlan;
using MicroservicioAsignacionCalendario.Application.Interfaces.AlumnoPlan;

namespace MicroservicioAsignacionCalendario.Application.Services
{
    public class AlumnoPlanService : IAlumnoPlanService
    {
        // TO DO: Agregar CQRS
        public AlumnoPlanService() { }

        // TO DO: Implementar método
        public async Task<AlumnoPlanResponse> AsignarPlanAsync(AlumnoPlanRequest req)
        {
            throw new NotImplementedException();
        }

        // TO DO: Implementar método
        public async Task<List<AlumnoPlanResponse>> ObtenerPlanesPorAlumnoAsync(Guid alumnoId)
        {
            throw new NotImplementedException();
        }
    }
}
