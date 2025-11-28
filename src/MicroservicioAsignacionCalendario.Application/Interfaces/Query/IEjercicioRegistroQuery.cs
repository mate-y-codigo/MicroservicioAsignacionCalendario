using MicroservicioAsignacionCalendario.Application.DTOs.EjercicioRegistro;
using MicroservicioAsignacionCalendario.Domain.Entities;

namespace MicroservicioAsignacionCalendario.Application.Interfaces.Query
{
    public interface IEjercicioRegistroQuery
    {
        Task<List<EjercicioRegistro>> ObtenerEjerciciosRegistros(EjercicioRegistroFilterRequest filtros);
    }
}
