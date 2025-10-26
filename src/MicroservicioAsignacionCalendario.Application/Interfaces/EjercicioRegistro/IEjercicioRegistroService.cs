using MicroservicioAsignacionCalendario.Application.DTOs.EjercicioRegistro;

namespace MicroservicioAsignacionCalendario.Application.Interfaces.RegistroEjercicio
{
    public interface IEjercicioRegistroService
    {
        Task<EjercicioRegistroResponse> RegistrarEjercicioAsync(EjercicioRegistroRequest registro);
        Task<List<EjercicioRegistroResponse>> ObtenerRegistrosAsync(Guid alumno_id, EjercicioRegistroFilterRequest filtros);
    }
}
