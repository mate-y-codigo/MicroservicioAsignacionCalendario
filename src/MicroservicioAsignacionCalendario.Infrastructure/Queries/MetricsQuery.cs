using Microsoft.EntityFrameworkCore;
using MicroservicioAsignacionCalendario.Infrastructure.Data;

// ENTIDADES
using MicroservicioAsignacionCalendario.Domain.Entities;

// INTERFAZ CORRECTA
using MicroservicioAsignacionCalendario.Application.Interfaces.metrics;



namespace MicroservicioAsignacionCalendario.Infrastructure.Queries
{
    public class MetricsQuery : IMetricsQuery
    {
        private readonly AppDbContext _context;

        public MetricsQuery(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<AlumnoPlan>> GetPlanesDelEntrenadorAsync(Guid idEntrenador)
        {
            return _context.AlumnoPlan
                .Where(p => p.IdEntrenador == idEntrenador)
                .ToListAsync();
        }

        public Task<List<SesionRealizada>> GetSesionesRealizadasAsync(Guid idEntrenador, DateTime desde, DateTime hasta)
        {
            return _context.SesionRealizada
                .Where(s =>
                    s.FechaRealizacion >= desde &&
                    s.FechaRealizacion <= hasta &&
                    _context.AlumnoPlan.Any(p => p.Id == s.IdAlumnoPlan &&
                                                 p.IdEntrenador == idEntrenador))
                .ToListAsync();
        }

        public Task<List<EjercicioRegistro>> GetEjerciciosRegistradosAsync(Guid idEntrenador, DateTime desde, DateTime hasta)
        {
            return _context.EjercicioRegistro
                .Where(e =>
                    e.FechaRealizacion >= desde &&
                    e.FechaRealizacion <= hasta &&
                    _context.AlumnoPlan.Any(p => p.Id == e.IdAlumnoPlan &&
                                                 p.IdEntrenador == idEntrenador))
                .ToListAsync();
        }

        public Task<List<EventoCalendario>> GetEventosProgramadosAsync(Guid idEntrenador, DateTime desde, DateTime hasta)
        {
            return _context.EventoCalendario
                .Where(ev =>
                    ev.FechaProgramada >= desde &&
                    ev.FechaProgramada <= hasta &&
                    _context.AlumnoPlan.Any(p => p.Id == ev.IdAlumnoPlan &&
                                                 p.IdEntrenador == idEntrenador))
                .ToListAsync();
        }

        public Task<List<RecordPersonal>> GetRecordsPersonalesAsync(Guid idEntrenador, DateTime desde, DateTime hasta)
        {
            return _context.RecordPersonal
                .Where(r =>
                    r.FechaRegistro >= desde &&
                    r.FechaRegistro <= hasta &&
                    _context.AlumnoPlan.Any(p => p.Id == r.IdAlumnoPlan &&
                                                 p.IdEntrenador == idEntrenador))
                .ToListAsync();
        }
    }
}
