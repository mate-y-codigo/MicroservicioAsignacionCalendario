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
            var query = _context.EventoCalendario.AsQueryable();

            if (filtros.IdAlumno.HasValue)
                query = query.Where(e => e.IdAlumno == filtros.IdAlumno);

            if (filtros.IdPlanEntrenamiento.HasValue)
                query = query.Where(e => e.IdSesionEntrenamiento == filtros.IdPlanEntrenamiento);

            if (filtros.IdAlumnoPlan.HasValue)
                query = query.Where(e => e.IdAlumnoPlan == filtros.IdAlumnoPlan);

            if (filtros.Desde.HasValue)
                query = query.Where(e => e.FechaInicio >= filtros.Desde);

            if (filtros.Hasta.HasValue)
                query = query.Where(e => e.FechaFin <= filtros.Hasta);

            return await query
                .OrderBy(e => e.FechaInicio)
                .ToListAsync();
        }
    }
}
