using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.Command;
using Application.Interfaces.EventoCalendario;
using MicroservicioAsignacionCalendario.Application.DTOs.EventoCalendario;
using MicroservicioAsignacionCalendario.Application.DTOs.PlanEntrenamiento;
using MicroservicioAsignacionCalendario.Application.Interfaces.Query;
using MicroservicioAsignacionCalendario.Domain.Entities;

namespace Application.Services
{
    public class EventoCalendarioService : IEventoCalendarioService
    {
        private readonly IEventoCalendarioCommand _eventoCommand;
        private readonly IEventoCalendarioQuery _query;


        public EventoCalendarioService(IEventoCalendarioCommand eventoCommand, IEventoCalendarioQuery query)
        {
            _eventoCommand = eventoCommand;
            _query = query;
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

        public async Task CrearPrimerEventoAsync(AlumnoPlan alumnoPlan, string nombreSesion)
        {
            var evento = new EventoCalendario
            {
                Id = Guid.NewGuid(),
                IdAlumnoPlan = alumnoPlan.Id,
                IdSesionEntrenamiento = alumnoPlan.IdSesionARealizar,
                NombreSesion = nombreSesion,
                FechaProgramada = alumnoPlan.FechaInicio,
                Estado = EstadoEvento.Programado,
                Notas = null
            };

            await _eventoCommand.InsertarEventoCalendario(evento);
        }


        public async Task CrearSiguienteEventoAsync(AlumnoPlan alumnoPlan, DateTime fechaBase, string nombreSesion)
        {
            // nueva fecha 
            var fechaProgramada = fechaBase
                .AddDays(alumnoPlan.IntervaloDiasDescanso + 1);


            if (fechaProgramada.Date > alumnoPlan.FechaFin.Date)
                return;

            var nuevoEvento = new EventoCalendario
            {
                Id = Guid.NewGuid(),
                IdAlumnoPlan = alumnoPlan.Id,
                IdSesionEntrenamiento = alumnoPlan.IdSesionARealizar,
                NombreSesion = nombreSesion,
                FechaProgramada = fechaProgramada,
                Estado = EstadoEvento.Programado,
                Notas = null
            };

            await _eventoCommand.InsertarEventoCalendario(nuevoEvento);
        }

        public async Task<List<EventoCalendarioResponse>> ObtenerEventosAsync(EventoCalendarioFilterRequest filtros)
        {
            var eventos = await _query.ObtenerEventos(filtros);

            return eventos.Select(e => new EventoCalendarioResponse
            {
                Id = e.Id,
                IdAlumnoPlan = e.IdAlumnoPlan,
                IdSesionEntrenamiento = e.IdSesionEntrenamiento,
                NombreSesion = e.NombreSesion,
                NombreAlumno = e.AlumnoPlan.NombreAlumno,
                FechaProgramada = e.FechaProgramada,
                Estado = e.Estado,
                Notas = e.Notas
            }).ToList();
        }


    }

}
