using MicroservicioAsignacionCalendario.Application.DTOs.EventoCalendario;

namespace MicroservicioAsignacionCalendario.Application.Interfaces.EventoCalendario
{
    public interface IEventoCalendarioService
    {
        Task<List<EventoCalendarioResponse>> ObtenerEventosAsync(EventoCalendarioFilterRequest filtros);
    }
}
