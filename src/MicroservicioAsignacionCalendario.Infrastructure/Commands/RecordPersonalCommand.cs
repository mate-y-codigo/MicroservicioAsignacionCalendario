using Application.Interfaces.Command;
using MicroservicioAsignacionCalendario.Domain.Entities;
using MicroservicioAsignacionCalendario.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicioAsignacionCalendario.Infrastructure.Commands
{
    public class RecordPersonalCommand : IRecordPersonalCommand
    {
        private readonly AppDbContext _context;

        public RecordPersonalCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task InsertarRecordPersonal(RecordPersonal record)
        {
            await _context.RecordPersonal.AddAsync(record);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarRecordPersonal(RecordPersonal record)
        {
            _context.RecordPersonal.Update(record);
            await _context.SaveChangesAsync();
        }
    }
}
