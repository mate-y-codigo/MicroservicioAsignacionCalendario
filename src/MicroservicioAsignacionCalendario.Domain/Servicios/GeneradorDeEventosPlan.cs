using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroservicioAsignacionCalendario.Application.DTOs.SesionEntrenamiento;
using MicroservicioAsignacionCalendario.Domain.Entities;

namespace MicroservicioAsignacionCalendario.Domain.Servicios
{
    public interface IGeneradorDeEventosPlan
    {
        IEnumerable<EventoCalendario> Generar(
            Guid idAlumnoPlan,
            Guid idAlumno,
            Guid idEntrenador,
            IReadOnlyList<SesionEntrenamientoResponse> sesionesOrdenadas,
            DateOnly fechaInicio,
            DateOnly? fechaFin,
            TimeOnly horaDefault,
            TimeSpan duracionDefault,
            int semanasVentanaDefault
        );
    }

    public class GeneradorDeEventosPlan : IGeneradorDeEventosPlan
    {
        public IEnumerable<EventoCalendario> Generar(
            Guid idAlumnoPlan,
            Guid idAlumno,
            Guid idEntrenador,
            IReadOnlyList<SesionEntrenamientoResponse> sesionesOrdenadas,
            DateOnly fechaInicio,
            DateOnly? fechaFin,
            TimeOnly horaDefault,
            TimeSpan duracionDefault,
            int semanasVentanaDefault)
        {
            if (sesionesOrdenadas is null || sesionesOrdenadas.Count == 0)
                yield break;

            var fin = fechaFin ?? fechaInicio.AddDays(7 * semanasVentanaDefault);
            var fecha = fechaInicio;
            var i = 0;

            while (fecha <= fin)
            {
                var sesion = sesionesOrdenadas[i];
                var inicio = fecha.ToDateTime(horaDefault, DateTimeKind.Unspecified);
                var finDT = inicio.Add(duracionDefault);

                yield return new EventoCalendario
                {
                    Id = Guid.NewGuid(),
                    IdAlumnoPlan = idAlumnoPlan,      // si tu entity lo tiene
                    IdAlumno = idAlumno,
                    IdEntrenador = idEntrenador,
                    IdSesionEntrenamiento = sesion.Id, // GUID externo
                    FechaInicio = new DateTimeOffset(inicio),
                    FechaFin = new DateTimeOffset(finDT),
                    Estado = Estado.Activo,            // 0
                    Notas = null,
                    FechaCreacion = DateTimeOffset.UtcNow,
                    FechaActualizacion = DateTimeOffset.UtcNow
                };

                i = (i + 1) % sesionesOrdenadas.Count;
                fecha = fecha.AddDays(1);
            }
        }
    }
}
