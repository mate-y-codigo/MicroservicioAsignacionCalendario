using Application.Interfaces.Query;
using MicroservicioAsignacionCalendario.Application.DTOs.SesionRealizada;
using MicroservicioAsignacionCalendario.Domain.Entities;
using MicroservicioAsignacionCalendario.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Queries
{
    public class SesionesRealizadasQuery : ISesionRealizadaQuery
    {
        private readonly AppDbContext _context;

        public SesionesRealizadasQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SesionRealizada> ObtenerSesionRealizadaPorId(Guid id)
        {
            return await _context.SesionRealizada.FirstOrDefaultAsync(sr => sr.Id == id);
        }

        public async Task<List<SesionRealizada>> ObtenerSesionesCompletadas(Guid alumnoPlanId)
        {
            return await _context.SesionRealizada
                .Where(sr => sr.IdAlumnoPlan == alumnoPlanId && sr.Estado == EstadoSesion.Completado)
                .OrderBy(sr => sr.OrdenSesion)
                .ToListAsync();
        }

        public async Task<List<SesionRealizada>> ObtenerSesionesRealizadas(SesionRealizadaFilterRequest filtros)
        {
            var query = _context.SesionRealizada
                .Include(sr => sr.EjerciciosRegistrados)
                .Include(sr => sr.AlumnoPlan)
                .AsNoTracking().AsQueryable();

            var idAlumno = string.IsNullOrEmpty(filtros.IdAlumno?.ToString()) ? Guid.Empty : filtros.IdAlumno.Value;
            var idPlan = string.IsNullOrEmpty(filtros.IdPlanEntrenamiento?.ToString()) ? Guid.Empty : filtros.IdPlanEntrenamiento.Value;
            var idSesion = string.IsNullOrEmpty(filtros.IdSesionEntrenamiento?.ToString()) ? Guid.Empty : filtros.IdSesionEntrenamiento.Value;

            query = query.Where(sr => sr.AlumnoPlan.IdEntrenador == filtros.IdEntrenador);

            if(idAlumno != Guid.Empty)
                query = query.Where(sr => sr.AlumnoPlan.IdAlumno == filtros.IdAlumno);

            if(idPlan != Guid.Empty)
                query = query.Where(sr => sr.AlumnoPlan.IdPlanEntrenamiento == filtros.IdPlanEntrenamiento);

            if (idSesion != Guid.Empty)
                query = query.Where(s => s.IdSesionEntrenamiento == filtros.IdSesionEntrenamiento);

            if (filtros.Desde.HasValue)
                query = query.Where(s => s.FechaRealizacion >= filtros.Desde);

            if (filtros.Hasta.HasValue)
                query = query.Where(s => s.FechaRealizacion <= filtros.Hasta);

            query = query.OrderByDescending(s => s.FechaRealizacion);
            return await query.ToListAsync();
        }
    }
}
