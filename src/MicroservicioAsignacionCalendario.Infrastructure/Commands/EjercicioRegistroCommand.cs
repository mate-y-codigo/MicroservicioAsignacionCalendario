using Application.Interfaces.Command;
using MicroservicioAsignacionCalendario.Domain.Entities;
using MicroservicioAsignacionCalendario.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicioAsignacionCalendario.Infrastructure.Commands
{
    public class EjercicioRegistroCommand : IEjercicioRegistroCommand
    {
        private readonly AppDbContext _context;

        public EjercicioRegistroCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task InsertarEjercicioRegistro(EjercicioRegistro ejercicioRegistro)
        {
            _context.EjercicioRegistro.Add(ejercicioRegistro);
            await _context.SaveChangesAsync();
        }
    }
}
