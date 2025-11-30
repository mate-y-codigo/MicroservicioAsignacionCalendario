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

        public async Task<RecordPersonal?> ObtenerRecordPersonalPorId(Guid idAlumno, Guid IdEjercicio)
        {
            return await _context.RecordPersonal
                .Include(r => r.AlumnoPlan)
                .Include(r => r.SesionRealizada)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.IdAlumno == idAlumno && r.IdEjercicio == IdEjercicio);
        }

        public async Task<List<RecordPersonal>> ObtenerRecordsPersonales(RecordPersonalFilterRequest filtros)
        {
            var query = _context.RecordPersonal
                .Include(r => r.AlumnoPlan)
                .Include(r => r.SesionRealizada)
                .AsNoTracking().AsQueryable();

            var idAlumno = string.IsNullOrEmpty(filtros.IdAlumno?.ToString()) ? Guid.Empty : filtros.IdAlumno.Value;
            var idEjercicio = string.IsNullOrEmpty(filtros.IdEjercicio?.ToString()) ? Guid.Empty : filtros.IdEjercicio.Value;

            if (idAlumno != Guid.Empty)
                query = query.Where(r => r.IdAlumno == filtros.IdAlumno);

            if (idEjercicio != Guid.Empty)
                query = query.Where(r => r.IdEjercicio == filtros.IdEjercicio);

            if (filtros.Desde.HasValue)
                query = query.Where(r => r.FechaRegistro.Date >= filtros.Desde);

            if (filtros.Hasta.HasValue)
                query = query.Where(r => r.FechaRegistro.Date <= filtros.Hasta);

            return await query.ToListAsync();
        }
    }
}
