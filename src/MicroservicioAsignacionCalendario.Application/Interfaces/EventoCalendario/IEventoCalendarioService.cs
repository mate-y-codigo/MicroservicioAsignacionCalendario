using Application.DTOs.EventoCalendario;
using MicroservicioAsignacionCalendario.Application.DTOs.EventoCalendario;

namespace Application.Interfaces.EventoCalendario
{
    public interface IEventoCalendarioService
    {
        Task<List<EventoCalendarioResponse>> ObtenerEventosAsync(EventoCalendarioFilterRequest filtros);
    }
}
