using Application.Interfaces.Query;
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
    }
}
