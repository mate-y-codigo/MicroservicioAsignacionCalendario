using MicroservicioAsignacionCalendario.Application.Interfaces.Query;
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
    }
}
