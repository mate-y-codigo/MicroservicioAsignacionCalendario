using Application.Interfaces.Command;
using MicroservicioAsignacionCalendario.Domain.Entities;
using MicroservicioAsignacionCalendario.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
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

        public async Task InsertarSesionRealizadaCompleta(SesionRealizada sesion, List<EjercicioRegistro> ejercicios, Guid idAlumno)
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

                    await ActualizarRecords(idAlumno, ejercicios, sesion.FechaRealizacion);

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

        private async Task ActualizarRecords(Guid idAlumno, List<EjercicioRegistro> nuevosEjercicios, DateTime fechaRegistro)
        {
            var idsEjercicios = nuevosEjercicios.Select(e => e.IdEjercicio).Distinct();

            var recordsExistentes = await _context.RecordPersonal
                .Where(r => r.IdAlumno == idAlumno && idsEjercicios.Contains(r.IdEjercicio))
                .ToDictionaryAsync(r => r.IdEjercicio);

            foreach (var ejercicio in nuevosEjercicios)
            {
                if (recordsExistentes.TryGetValue(ejercicio.IdEjercicio, out var recordExistente))
                {
                    bool esNuevoRecord = false;

                    if (ejercicio.Peso > recordExistente.PesoMax)
                        esNuevoRecord = true;
                    else if (ejercicio.Peso == recordExistente.PesoMax && ejercicio.Repeticiones > recordExistente.Repeticiones)
                        esNuevoRecord = true;

                    if (esNuevoRecord)
                    {
                        recordExistente.PesoMax = ejercicio.Peso;
                        recordExistente.Repeticiones = ejercicio.Repeticiones;
                        recordExistente.Series = ejercicio.Series;
                        recordExistente.FechaRegistro = fechaRegistro;
                        recordExistente.NombreEjercicio = ejercicio.NombreEjercicio;

                        _context.RecordPersonal.Update(recordExistente);
                    }
                }
                else
                {
                    var nuevoRecord = new RecordPersonal
                    {
                        IdAlumno = idAlumno,
                        IdEjercicio = ejercicio.IdEjercicio,
                        PesoMax = ejercicio.Peso,
                        Repeticiones = ejercicio.Repeticiones,
                        Series = ejercicio.Series,
                        FechaRegistro = fechaRegistro,
                        NombreEjercicio = ejercicio.NombreEjercicio
                    };

                    await _context.RecordPersonal.AddAsync(nuevoRecord);
                    recordsExistentes.Add(ejercicio.IdEjercicio, nuevoRecord);
                }
            }
        }
    }
}
