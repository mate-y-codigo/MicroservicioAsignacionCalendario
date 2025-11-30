using Interfaces.Query;
using MicroservicioAsignacionCalendario.Application.DTOs.AlumnoPlan;
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

        public async Task<AlumnoPlan> ObtenerAlumnoPlanPorId(Guid id)
        {
            return await _context.AlumnoPlan
                .Include(ap => ap.SesionesRealizadas)
                .ThenInclude(sr => sr.EjerciciosRegistrados)
                .FirstOrDefaultAsync(ap => ap.Id == id);
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

        public async Task<List<AlumnoPlan>> ObtenerPlanesConFiltros(AlumnoPlanFilterRequest filtros)
        {
            var query = _context.AlumnoPlan.AsNoTracking().AsQueryable();

            if (filtros.IdEntrenador != null)
                query = query.Where(ap => ap.IdEntrenador == filtros.IdEntrenador);

            if (filtros.IdAlumno != null)
                query = query.Where(ap => ap.IdAlumno == filtros.IdAlumno);

            if(filtros.IdPlanEntrenamiento != null)
                query = query.Where(ap => ap.IdPlanEntrenamiento == filtros.IdPlanEntrenamiento);

            if(filtros.Estado != null)
                query = query.Where(ap => ap.Estado == filtros.Estado);

            if(filtros.Desde != null)
                query = query.Where(ap => ap.FechaInicio >= filtros.Desde);

            if(filtros.Hasta != null)
                query = query.Where(ap => ap.FechaFin <= filtros.Hasta);

            return await query.Include(p => p.SesionesRealizadas)
                .ThenInclude(sr => sr.EjerciciosRegistrados)
                .ToListAsync();
        }
    }
}
