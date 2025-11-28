using System.Globalization;
using MicroservicioAsignacionCalendario.Application.DTOs.metricas;
using MicroservicioAsignacionCalendario.Application.Interfaces.Query;



namespace MicroservicioAsignacionCalendario.Application.Services
{
    public class MetricsService : IMetricsService
    {
        private readonly IMetricsQuery _metricsQuery;

        public MetricsService(IMetricsQuery metricsQuery)
        {
            _metricsQuery = metricsQuery;
        }

        public async Task<MetricaResponseDto> GetMetricasGrupalesAsync(
            Guid idEntrenador, DateTime desde, DateTime hasta)
        {
            // 🔥 Consultas
            var planes = await _metricsQuery.GetPlanesDelEntrenadorAsync(idEntrenador);
            var sesiones = await _metricsQuery.GetSesionesRealizadasAsync(idEntrenador, desde, hasta);
            var ejercicios = await _metricsQuery.GetEjerciciosRegistradosAsync(idEntrenador, desde, hasta);
            var eventos = await _metricsQuery.GetEventosProgramadosAsync(idEntrenador, desde, hasta);
            var records = await _metricsQuery.GetRecordsPersonalesAsync(idEntrenador, desde, hasta);

            // ===================================
            // 1) Cumplimiento Global
            // ===================================
            var prog = ejercicios.Count;
            var comp = ejercicios.Count(e => e.Series >= e.SeriesObjetivo);

            double cumplimientoGlobal = prog == 0 ? 0 : (double)comp / prog * 100;

            var grafiquitos = new List<GraficoItemDto>
            {
                new()
                {
                    Title = "Cumplimiento Global",
                    Value = $"{cumplimientoGlobal:0}%",
                    Delta = "+5% vs anterior",
                    Color = "#4f46e5"
                }
            };

            // ===================================
            // 2) Carga Semanal
            // ===================================
            var cargas = ejercicios
                .GroupBy(e => ISOWeek.GetWeekOfYear(e.FechaRealizacion))
                .Select(g => new LoadDataDto
                {
                    Week = $"Sem {g.Key}",
                    Load = g.Sum(e => (double)e.Peso * (double)e.Repeticiones * (double)e.Series)

                }).ToList();

            grafiquitos.Add(new GraficoItemDto
            {
                Title = "Carga Total Semanal",
                Value = $"{(cargas.LastOrDefault()?.Load ?? 0) / 1000:0.0}k kg",
                Delta = "+4.2% vs anterior",
                Color = "#06b6d4"
            });

            var fuerza = ejercicios
                .Select(e =>
                {
                    var rm = e.Peso * (1m + 0.0333m * e.Repeticiones);
                    return new { Week = ISOWeek.GetWeekOfYear(e.FechaRealizacion), RM = rm };
                })
                .GroupBy(x => x.Week)
                .Select(g => new StrengthDataDto
                {
                    Week = $"Sem {g.Key}",
                    Avg1RM = (double)g.Average(x => x.RM)
                })
                .ToList();

            grafiquitos.Add(new GraficoItemDto
            {
                Title = "Progreso Fuerza",
                Value = $"{fuerza.LastOrDefault()?.Avg1RM:0}",
                Delta = "+3% fuerza",
                Color = "#10b981"
            });

            // ===================================
            // Armado final
            // ===================================

            return new MetricaResponseDto
            {
                Grafiquitos = grafiquitos,
                StrengthData = fuerza,
                LoadData = cargas,
                ComplianceHistory = new List<ComplianceHistoryDto>(),
                WeeklyCompliance = new List<WeeklyComplianceDto>(),
                SessionGapData = new List<SessionGapDataDto>(),
                Prs = new List<PrsDto>()
            };
        }

        public Task<MetricaResponseDto> GetMetricasAlumnoAsync(Guid idAlumno, DateTime desde, DateTime hasta)
        {
            return GetMetricasGrupalesAsync(idAlumno, desde, hasta);
        }
    }
}
