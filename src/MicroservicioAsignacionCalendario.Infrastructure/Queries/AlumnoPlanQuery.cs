using Interfaces.Query;
using MicroservicioAsignacionCalendario.Domain.Entities;
using MicroservicioAsignacionCalendario.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroservicioAsignacionCalendario.Infrastructure.Queries
{
    public class AlumnoPlanQuery : IAlumnoPlanQuery
    {
        private readonly AppDbContext _context;
        public AlumnoPlanQuery(AppDbContext context)
        {
            _context = context;
        }
        public async Task<AlumnoPlan> ObtenerAlumnoPlan(Guid idAlumno)
        {
            return await _context.AlumnoPlan
                .FirstOrDefaultAsync(ap => ap.IdAlumno == idAlumno && ap.Estado == EstadoAlumnoPlan.Activo);
        }

        public async Task<bool> PlanEntrenamientoAsignado(Guid idPlanEntrenamiento)
        {
            return await _context.AlumnoPlan.AnyAsync(ap => ap.IdPlanEntrenamiento == idPlanEntrenamiento && ap.Estado == EstadoAlumnoPlan.Activo);
        }

        public async Task<List<AlumnoPlan>> ObtenerPlanesPorAlumno(Guid IdAlumno)
        {
            return await _context.AlumnoPlan.AsNoTracking()
                .Where(Pa => Pa.IdAlumno == IdAlumno && Pa.Estado == EstadoAlumnoPlan.Activo)
                .Include(Pa => Pa.EventosCalendarios)
                .Include(Pa => Pa.SesionesRealizadas)
                .ToListAsync();
        }
    }
}
