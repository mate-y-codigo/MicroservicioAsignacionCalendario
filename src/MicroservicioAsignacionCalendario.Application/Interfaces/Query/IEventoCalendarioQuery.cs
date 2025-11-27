using MicroservicioAsignacionCalendario.Application.DTOs.EventoCalendario;
using MicroservicioAsignacionCalendario.Domain.Entities;

namespace MicroservicioAsignacionCalendario.Application.Interfaces.Query
{
    public interface IEventoCalendarioQuery
    {
        Task<List<EventoCalendario>> ObtenerEventos(EventoCalendarioFilterRequest filtros);
    }
}
