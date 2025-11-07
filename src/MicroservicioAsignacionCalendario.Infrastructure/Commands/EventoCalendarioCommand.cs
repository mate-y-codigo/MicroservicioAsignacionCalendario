using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces.Command;
using MicroservicioAsignacionCalendario.Domain.Entities;
using MicroservicioAsignacionCalendario.Infrastructure.Data;

namespace MicroservicioAsignacionCalendario.Infrastructure.Persistence.Commands
{
    public sealed class EventoCalendarioCommand : IEventoCalendarioCommand
    {
        private readonly AppDbContext _context;

        public EventoCalendarioCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task InsertarEventosCalendario(IEnumerable<EventoCalendario> eventos)
        {
            // Agregamos todos los eventos al contexto
            await _context.EventoCalendario.AddRangeAsync(eventos);
            // Persistimos en la BD
            await _context.SaveChangesAsync();
        }
    }
}
