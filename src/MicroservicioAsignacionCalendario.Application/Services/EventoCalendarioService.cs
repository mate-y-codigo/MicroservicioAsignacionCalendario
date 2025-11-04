using Application.DTOs.EventoCalendario;
using Application.Interfaces.EventoCalendario;
using MicroservicioAsignacionCalendario.Application.DTOs.EventoCalendario;

namespace Application.Services
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
