using Application.Interfaces.Command;
using MicroservicioAsignacionCalendario.Domain.Entities;
using MicroservicioAsignacionCalendario.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Commands
{
    public class AlumnoPlanCommand : IAlumnoPlanCommand
    {
        private readonly AppDbContext _context;
        public AlumnoPlanCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task InsertarAlumnoPlan(AlumnoPlan alumnoPlan)
        {
            _context.Add(alumnoPlan);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAlumnoPlan(AlumnoPlan alumnoPlan)
        {
            _context.AlumnoPlan.Update(alumnoPlan);
            await _context.SaveChangesAsync();
        }
    }
}
