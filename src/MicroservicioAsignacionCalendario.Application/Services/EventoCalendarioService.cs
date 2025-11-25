using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.Command;
using Application.Interfaces.EventoCalendario;
using MicroservicioAsignacionCalendario.Application.DTOs.EventoCalendario;
using MicroservicioAsignacionCalendario.Application.DTOs.PlanEntrenamiento;
using MicroservicioAsignacionCalendario.Domain.Entities;

namespace Application.Services
{
    public class EventoCalendarioService : IEventoCalendarioService
    {
        private readonly IEventoCalendarioCommand _eventoCommand;

        public EventoCalendarioService(IEventoCalendarioCommand eventoCommand)
        {
            _eventoCommand = eventoCommand;
        }

        public async Task<List<EventoCalendarioResponse>> ObtenerEventosAsync(EventoCalendarioFilterRequest filtros)
        {
            throw new NotImplementedException();
        }



        public async Task CrearEventosDePlanAsync(AlumnoPlan alumnoPlan, PlanEntrenamientoResponse plan)
        {
            if (plan.SesionesEntrenamiento == null || !plan.SesionesEntrenamiento.Any())
                return;

            var sesiones = plan.SesionesEntrenamiento.OrderBy(s => s.Orden).ToList();
            var eventos = new List<EventoCalendario>();

            var fecha = alumnoPlan.FechaInicio.Date;
            var fechaFin = alumnoPlan.FechaFin.Date;
            int idx = 0;

            while (fecha <= fechaFin)
            {
                var sesion = sesiones[idx];
                var evento = new EventoCalendario
                {
                    Id = Guid.NewGuid(),
                    //IdAlumno = alumnoPlan.IdAlumno,
                    //IdEntrenador = plan.IdEntrenador,
                    IdSesionEntrenamiento = sesion.Id,
                    IdAlumnoPlan = alumnoPlan.Id,
                    //FechaInicio = fecha,
                    //FechaFin = fecha.AddHours(1),
                    Estado = EstadoEvento.Programado,
                    Notas = $"Sesión {sesion.Orden} - {plan.Nombre}"
                };

                eventos.Add(evento);

                fecha = fecha.AddDays(1 + alumnoPlan.IntervaloDiasDescanso);
                idx = (idx + 1) % sesiones.Count;
            }

            await _eventoCommand.InsertarEventosCalendario(eventos);
        }

        public async Task CrearPrimerEventoAsync(AlumnoPlan alumnoPlan)
        {
            var evento = new EventoCalendario
            {
                Id = Guid.NewGuid(),
                IdAlumnoPlan = alumnoPlan.Id,
                IdSesionEntrenamiento = alumnoPlan.IdSesionARealizar,
                FechaProgramada = alumnoPlan.FechaInicio,
                Estado = EstadoEvento.Programado,
                Notas = null
            };

            await _eventoCommand.InsertarEventoCalendario(evento);
        }
    }
}
