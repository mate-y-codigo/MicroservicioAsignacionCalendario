using MicroservicioAsignacionCalendario.Application.DTOs.EventoCalendario;
using MicroservicioAsignacionCalendario.Application.DTOs.Metricas;
using MicroservicioAsignacionCalendario.Application.Interfaces.Metricas;
using MicroservicioAsignacionCalendario.Application.Interfaces.Query;
using MicroservicioAsignacionCalendario.Domain.Entities;

namespace MicroservicioAsignacionCalendario.Application.Services
{
    public class MetricasService : IMetricasService
    {
        private readonly IEventoCalendarioQuery _eventoCalendarioQuery;

        public MetricasService(IEventoCalendarioQuery eventoCalendarioQuery)
        {
            _eventoCalendarioQuery = eventoCalendarioQuery;
        }

        public async Task<MetricasGrupalesResponse> ObtenerMetricasGrupalesAsync(
            Guid idEntrenador,
            DateTime desde,
            DateTime hasta)
        {
            // Normalizamos fechas a solo fecha (sin hora)
            var desdeDate = DateTime.SpecifyKind(desde.Date, DateTimeKind.Utc);
            var hastaDate = DateTime.SpecifyKind(hasta.Date.AddDays(1).AddTicks(-1), DateTimeKind.Utc);

            // 1) Traer eventos del entrenador en el rango
            var filtros = new EventoCalendarioFilterRequest
            {
                IdEntrenador = idEntrenador,
                Desde = desdeDate,
                Hasta = hastaDate
            };

            var eventos = await _eventoCalendarioQuery.ObtenerEventos(filtros);

            // 2) Agrupar por día: programados y completados
            var agrupado = eventos
                .GroupBy(e => e.FechaProgramada.Date)
                .Select(g =>
                {
                    var programados = g.Count();
                    var completados = g.Count(e => e.Estado == EstadoEvento.Completado);

                    decimal porcentaje = programados > 0
                        ? (decimal)completados / programados * 100m
                        : 0m;

                    return new CumplimientoPorDiaItemDto
                    {
                        Fecha = g.Key,
                        Programados = programados,
                        Completados = completados,
                        Porcentaje = Math.Round(porcentaje, 2)
                    };
                })
                .OrderBy(x => x.Fecha)
                .ToList();

            // 3) Porcentaje global en el periodo (sobre todos los eventos)
            var totalProgramados = agrupado.Sum(x => x.Programados);
            var totalCompletados = agrupado.Sum(x => x.Completados);

            decimal porcentajeGlobal = totalProgramados > 0
                ? (decimal)totalCompletados / totalProgramados * 100m
                : 0m;

            var cumplimiento = new CumplimientoDiarioDto
            {
                PorcentajeGlobal = Math.Round(porcentajeGlobal, 2),
                Serie = agrupado
            };

            return new MetricasGrupalesResponse
            {
                Cumplimiento = cumplimiento
            };
        }
    }
}
