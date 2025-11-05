using MicroservicioAsignacionCalendario.Application.DTOs.EjercicioRegistro;
using MicroservicioAsignacionCalendario.Application.Interfaces.Query;
using MicroservicioAsignacionCalendario.Domain.Entities;
using MicroservicioAsignacionCalendario.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicioAsignacionCalendario.Infrastructure.Queries
{
    public class EjercicioRegistroQuery : IEjercicioRegistroQuery
    {
        private readonly AppDbContext _context;

        public EjercicioRegistroQuery(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<List<EjercicioRegistro>> ObtenerEjerciciosRegistros(EjercicioRegistroFilterRequest filtros)
        {
            var query = _context.EjercicioRegistro.AsNoTracking().AsQueryable();

            if (filtros.IdEjercicio.HasValue)
                query = query.Where(er => er.IdEjercicio == filtros.IdEjercicio);

            if (filtros.IdSesionEntrenamiento.HasValue)
                query = query.Where(er => er.SesionRealizada.IdSesionEntrenamiento == filtros.IdSesionEntrenamiento);

            if (filtros.IdAlumno.HasValue)
                query = query.Where(er => er.SesionRealizada.AlumnoPlan.IdAlumno == filtros.IdAlumno);

            return await query.ToListAsync();
        }
    }
}
