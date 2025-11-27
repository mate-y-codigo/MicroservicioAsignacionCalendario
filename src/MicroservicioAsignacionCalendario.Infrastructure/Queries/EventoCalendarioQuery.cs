using Microsoft.EntityFrameworkCore;
using MicroservicioAsignacionCalendario.Application.DTOs.EventoCalendario;
using MicroservicioAsignacionCalendario.Application.Interfaces.Query;
using MicroservicioAsignacionCalendario.Domain.Entities;
using MicroservicioAsignacionCalendario.Infrastructure.Data;

namespace MicroservicioAsignacionCalendario.Infrastructure.Persistence.Queries
{
    public class EventoCalendarioQuery : IEventoCalendarioQuery
    {
        private readonly AppDbContext _context;

        public EventoCalendarioQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EventoCalendario>> ObtenerEventos(EventoCalendarioFilterRequest filtros)
        {
            var query = _context.EventoCalendario
                .Include(e => e.AlumnoPlan)
                .AsQueryable();

            if (filtros.IdAlumno.HasValue)
                query = query.Where(e => e.AlumnoPlan.IdAlumno == filtros.IdAlumno.Value);


            if (filtros.IdEntrenador.HasValue)
                query = query.Where(e => e.AlumnoPlan.IdEntrenador == filtros.IdEntrenador.Value);

            if (filtros.IdSesionEntrenamiento.HasValue)
                query = query.Where(e => e.IdSesionEntrenamiento == filtros.IdSesionEntrenamiento.Value);


            if (filtros.Estado.HasValue)
                query = query.Where(e => e.Estado == filtros.Estado.Value);

            // texto en notas
            if (!string.IsNullOrWhiteSpace(filtros.Notas))
                query = query.Where(e => e.Notas!.Contains(filtros.Notas));

            // fecha
            if (filtros.Desde.HasValue)
                query = query.Where(e => e.FechaProgramada >= filtros.Desde.Value);

            if (filtros.Hasta.HasValue)
                query = query.Where(e => e.FechaProgramada <= filtros.Hasta.Value);

            return await query
                .OrderBy(e => e.FechaProgramada)
                .ToListAsync();
        }
    }
}
