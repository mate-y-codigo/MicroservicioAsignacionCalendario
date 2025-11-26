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
            throw new NotImplementedException();
            //var query = _context.SesionRealizada.AsNoTracking().AsQueryable();

            //query = query.Where(s => s.AlumnoPlan.IdAlumno == filtros.IdAlumno);
            //query = query.Where(s => s.AlumnoPlan.IdPlanEntrenamiento == filtros.IdPlanEntrenamiento);

            //if (filtros.IdSesionEntrenamiento.HasValue)
            //    query = query.Where(s => s.IdSesionEntrenamiento == filtros.IdSesionEntrenamiento);

            //if (filtros.Desde.HasValue)
            //    query = query.Where(s => s.FechaRealizacion.Date >= filtros.Desde);

            //if (filtros.Hasta.HasValue)
            //    query = query.Where(s => s.FechaRealizacion.Date <= filtros.Hasta);

            //query = query.OrderByDescending(s => s.FechaRealizacion);
            //return await query.ToListAsync();
        }
    }
}
