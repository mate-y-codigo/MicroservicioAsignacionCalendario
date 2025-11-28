using Microsoft.EntityFrameworkCore;
using MicroservicioAsignacionCalendario.Infrastructure.Data;
using MicroservicioAsignacionCalendario.Domain.Entities;
using MicroservicioAsignacionCalendario.Application.Interfaces.Query;



namespace MicroservicioAsignacionCalendario.Infrastructure.Queries
{
    public class MetricsQuery : IMetricsQuery
    {
        private readonly AppDbContext _context;

        public MetricsQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AlumnoPlan>> GetPlanesDelEntrenadorAsync(Guid idEntrenador)
        {
            return await _context.AlumnoPlan
                .Where(ap => ap.IdEntrenador == idEntrenador)
                .ToListAsync();
        }

        public async Task<List<SesionRealizada>> GetSesionesRealizadasAsync(Guid idEntrenador, DateTime desde, DateTime hasta)
        {
            return await _context.SesionRealizada
                .Include(s => s.AlumnoPlan)
                .Where(s =>
                    s.AlumnoPlan.IdEntrenador == idEntrenador &&
                    s.FechaRealizacion != null &&
                    s.FechaRealizacion >= desde &&
                    s.FechaRealizacion <= hasta)
                .ToListAsync();
        }

        public async Task<List<EjercicioRegistro>> GetEjerciciosRegistradosAsync(Guid idEntrenador, DateTime desde, DateTime hasta)
        {
            return await _context.EjercicioRegistro
                .Include(er => er.SesionRealizada)
                .ThenInclude(sr => sr.AlumnoPlan)
                .Where(er =>
                    er.FechaRealizacion >= desde &&
                    er.FechaRealizacion <= hasta &&
                    er.SesionRealizada.AlumnoPlan.IdEntrenador == idEntrenador)
                .ToListAsync();
        }

        public async Task<List<EventoCalendario>> GetEventosProgramadosAsync(Guid idEntrenador, DateTime desde, DateTime hasta)
        {
            return await _context.EventoCalendario
                .Include(ec => ec.AlumnoPlan)
                .Where(ec =>
                    ec.AlumnoPlan.IdEntrenador == idEntrenador &&
                    ec.FechaProgramada >= desde &&
                    ec.FechaProgramada <= hasta)
                .ToListAsync();
        }

        public async Task<List<RecordPersonal>> GetRecordsPersonalesAsync(Guid idEntrenador, DateTime desde, DateTime hasta)
        {
            return await _context.RecordPersonal
                .Include(rp => rp.AlumnoPlan)
                .Where(rp =>
                    rp.AlumnoPlan.IdEntrenador == idEntrenador &&
                    rp.FechaRegistro >= desde &&
                    rp.FechaRegistro <= hasta)
                .ToListAsync();
        }
    }
}


