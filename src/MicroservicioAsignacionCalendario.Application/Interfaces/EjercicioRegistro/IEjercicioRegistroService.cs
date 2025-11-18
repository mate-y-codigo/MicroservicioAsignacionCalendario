using MicroservicioAsignacionCalendario.Application.DTOs.EjercicioRegistro;

namespace MicroservicioAsignacionCalendario.Application.Interfaces.RegistroEjercicio
{
    public interface IEjercicioRegistroService
    {
        Task<List<EjercicioRegistroResponse>> ObtenerRegistrosAsync(EjercicioRegistroFilterRequest filtros);
    }
}
