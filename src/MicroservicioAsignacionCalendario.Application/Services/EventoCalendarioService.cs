using MicroservicioAsignacionCalendario.Application.DTOs.EventoCalendario;
using MicroservicioAsignacionCalendario.Application.Interfaces.EventoCalendario;

namespace MicroservicioAsignacionCalendario.Application.Services
{
    public class EventoCalendarioService : IEventoCalendarioService
    {
        // TO DO: Agregar CQRS
        public EventoCalendarioService() { }

        // TO DO: Implementar método
        public async Task<List<EventoCalendarioResponse>> ObtenerEventosAsync(EventoCalendarioFilterRequest filtros)
        {
            throw new NotImplementedException();
        }
    }
}
