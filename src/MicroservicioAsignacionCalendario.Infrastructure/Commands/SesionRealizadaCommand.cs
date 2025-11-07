using Application.Interfaces.Command;
using MicroservicioAsignacionCalendario.Domain.Entities;
using MicroservicioAsignacionCalendario.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Commands
{
    public class SesionRealizadaCommand : ISesionRealizadaCommand
    {
        private readonly AppDbContext _context;

        public SesionRealizadaCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task InsertarSesionRealizadaCompleta(SesionRealizada sesion, List<EjercicioRegistro> ejercicios)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.SesionRealizada.AddAsync(sesion);
                    await _context.SaveChangesAsync();

                    foreach (var ejercicio in ejercicios)
                    {
                        ejercicio.IdSesionRealizada = sesion.Id;
                    }

                    await _context.EjercicioRegistro.AddRangeAsync(ejercicios);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
