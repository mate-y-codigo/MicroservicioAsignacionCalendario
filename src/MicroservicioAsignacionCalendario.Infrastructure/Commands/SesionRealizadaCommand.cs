using Application.Interfaces.Command;
using MicroservicioAsignacionCalendario.Application.CustomExceptions;
using MicroservicioAsignacionCalendario.Application.DTOs.PlanEntrenamiento;
using MicroservicioAsignacionCalendario.Domain.Entities;
using MicroservicioAsignacionCalendario.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public async Task InsertarSesionRealizadaCompleta(SesionRealizada sesion)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.SesionRealizada.AddAsync(sesion);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (DbUpdateException dbEx)
                {
                    await transaction.RollbackAsync();

                    if (dbEx.InnerException is Npgsql.PostgresException postgresEx)
                    {
                        if (postgresEx.SqlState == "23505")
                            throw new ConflictException("No se pudo guardar. Ya existe un registro similar o la clave ya está en uso.");
                        throw new BadRequestException($"Error de datos al guardar la sesión: {postgresEx.Detail}");
                    }
                    throw new Exception("Fallo interno de persistencia de datos.", dbEx);
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
