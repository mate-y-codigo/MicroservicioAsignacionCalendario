using Application.Interfaces.Query;
using MicroservicioAsignacionCalendario.Application.DTOs.RecordPersonal;
using MicroservicioAsignacionCalendario.Application.Interfaces.Query;
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
    public class RecordPersonalQuery : IRecordPersonalQuery
    {
        private readonly AppDbContext _context;

        public RecordPersonalQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RecordPersonal?> ObtenerRecordPersonalPorId(Guid idAlumnoPlan, Guid IdEjercicio)
        {
            return await _context.RecordPersonal.AsNoTracking().Include(r => r.AlumnoPlan)
                .FirstOrDefaultAsync(r => r.IdAlumnoPlan == idAlumnoPlan && r.IdEjercicio == IdEjercicio);
        }

        public async Task<List<RecordPersonal>> ObtenerRecordsPersonales(RecordPersonalFilterRequest filtros)
        {
            var query = _context.RecordPersonal.AsNoTracking().AsQueryable();

            query = query.Where(r => r.IdAlumno == filtros.IdAlumno);
            
            if (filtros.IdEjercicio.HasValue)
                query = query.Where(r => r.IdEjercicio == filtros.IdEjercicio);
            //if (filtros.PesoMinimo.HasValue)
            //    query = query.Where(r => r.PesoMax >= filtros.PesoMinimo);
            //if (filtros.RepeticionesMinimas.HasValue)
            //    query = query.Where(r => r.Repeticiones >= filtros.RepeticionesMinimas);
            if (filtros.Desde.HasValue)
                query = query.Where(r => r.FechaRegistro.Date >= filtros.Desde);
            if (filtros.Hasta.HasValue)
                query = query.Where(r => r.FechaRegistro.Date <= filtros.Hasta);

            bool esDescendente = filtros.Orden?.ToLower() == "desc";

            switch (filtros.OrdenarPor?.ToLower())
            {
                case "Peso":
                    query = esDescendente ? query.OrderByDescending(r => r.PesoMax) : query.OrderBy(r => r.PesoMax);
                    break;
                case "Repeticiones":
                    query = esDescendente ? query.OrderByDescending(r => r.Repeticiones) : query.OrderBy(r => r.Repeticiones);
                    break;
                default:
                    query = esDescendente ? query.OrderByDescending(r => r.FechaRegistro) : query.OrderBy(r => r.FechaRegistro);
                    break;
            }

            return await query.ToListAsync();
        }
    }
}
