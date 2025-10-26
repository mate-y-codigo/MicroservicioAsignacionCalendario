using MicroservicioAsignacionCalendario.Application.DTOs.EjercicioRegistro;
using MicroservicioAsignacionCalendario.Application.Interfaces.RegistroEjercicio;

namespace MicroservicioAsignacionCalendario.Application.Services
{
    public class EjercicioRegistroService : IEjercicioRegistroService
    {
        // TO DO: Agregar CQRS
        public EjercicioRegistroService() { }

        // TO DO: Implementar método
        public Task<EjercicioRegistroResponse> RegistrarEjercicioAsync(EjercicioRegistroRequest req)
        {
            throw new NotImplementedException();
        }

        // TO DO: Implementar método
        public Task<List<EjercicioRegistroResponse>> ObtenerRegistrosAsync(Guid alumno_id, EjercicioRegistroFilterRequest filtros)
        {
            throw new NotImplementedException();
        }
    }
}
