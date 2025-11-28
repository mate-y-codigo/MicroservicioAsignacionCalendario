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

        public Task<MetricaResponseDto> GetMetricasAlumnoAsync(Guid idAlumno, DateTime desde, DateTime hasta)
        {
            // Si Alumno = Entrenador en tu modelo: usar el mismo método
            return GetMetricasGrupalesAsync(idAlumno, desde, hasta);
        }

        public async Task<MetricaResponseDto> GetMetricasGrupalesAsync(
            Guid idEntrenador, DateTime desde, DateTime hasta)
        {
            // ============================
            // 🔵 1. Obtener datos reales
            // ============================
            var planes = await _metricsQuery.GetPlanesDelEntrenadorAsync(idEntrenador);
            var sesiones = await _metricsQuery.GetSesionesRealizadasAsync(idEntrenador, desde, hasta);
            var ejercicios = await _metricsQuery.GetEjerciciosRegistradosAsync(idEntrenador, desde, hasta);
            var eventos = await _metricsQuery.GetEventosProgramadosAsync(idEntrenador, desde, hasta);
            var records = await _metricsQuery.GetRecordsPersonalesAsync(idEntrenador, desde, hasta);

            // ============================
            // 🔵 2. Cumplimiento global
            // ============================
            var prog = ejercicios.Count;
            var comp = ejercicios.Count(e => e.Series >= e.SeriesObjetivo);
            double cumplimientoGlobal = prog == 0 ? 0 : (double)comp / prog * 100;

            // ============================
            // 🔵 3. Carga semanal (kg totales)
            // ============================
            var loadData = ejercicios
                .GroupBy(e => ISOWeek.GetWeekOfYear(e.FechaRealizacion))
                .Select(g => new LoadDataDto
                {
                    Week = $"Sem {g.Key}",
                    Load = g.Sum(e => (double)e.Peso * e.Repeticiones * e.Series)
                })
                .ToList();

            // ============================
            // 🔵 4. Fuerza (1RM promedio)
            // ============================
            var strengthData = ejercicios
                .Select(e => new
                {
                    week = ISOWeek.GetWeekOfYear(e.FechaRealizacion),
                    rm = (double)(e.Peso * (1m + 0.0333m * e.Repeticiones))
                })
                .GroupBy(g => g.week)
                .Select(g => new StrengthDataDto
                {
                    Week = $"Sem {g.Key}",
                    Avg1RM = g.Average(x => x.rm)
                })
                .ToList();

            // ============================
            // 🔵 5. Compliance History
            // ============================
            var complianceHistory = ejercicios
                .GroupBy(e => ISOWeek.GetWeekOfYear(e.FechaRealizacion))
                .Select(g =>
                {
                    var total = g.Count();
                    var completados = g.Count(x => x.Series >= x.SeriesObjetivo);

                    return new ComplianceHistoryDto
                    {
                        Date = $"Sem {g.Key}",
                        Compliance = total == 0 ? 0 : (double)completados / total * 100
                    };
                })
                .ToList();

            // ============================
            // 🔵 6. Weekly Compliance (L-M-X-J-V-S-D)
            // ============================
            var weeklyCompliance = sesiones
                .Where(s => s.FechaRealizacion != null)
                .GroupBy(s => s.FechaRealizacion!.Value.DayOfWeek)
                .Select(g => new WeeklyComplianceDto
                {
                    Day = GetDiaSemana(g.Key),
                    Value = g.Count()
                })
                .ToList();

            // ============================
            // 🔵 7. Session Gap (días entre sesiones)
            // ============================
            var orderedSessions = sesiones
                .Where(s => s.FechaRealizacion != null)
                .OrderBy(s => s.FechaRealizacion)
                .ToList();

            var sessionGapData = new List<SessionGapDataDto>();

            for (int i = 1; i < orderedSessions.Count; i++)
            {
                var prev = orderedSessions[i - 1].FechaRealizacion!.Value;
                var curr = orderedSessions[i].FechaRealizacion!.Value;

                sessionGapData.Add(new SessionGapDataDto
                {
                    Week = $"Sem {ISOWeek.GetWeekOfYear(curr)}",
                    Days = (curr - prev).TotalDays
                });
            }


            //PRs

            var prs = records
                .GroupBy(r => r.NombreEjercicio)
                .Select(g => new PrsDto
                {
                    Name = g.Key,
                    Prs = g.Count()
                })
                .ToList();


            //  Pequeños gráficos arriba
            // =======================================
            // PERIODOS
            // =======================================
            var prevDesde = desde.AddMonths(-3);
            var prevHasta = desde;

            // =======================================
            // DATOS: ACTUAL
            // =======================================
            var ejerciciosActual = ejercicios;
            var sesionesActual = sesiones;

            // =======================================
            // DATOS: TRIMESTRE ANTERIOR
            // =======================================
            var ejerciciosPrev = await _metricsQuery.GetEjerciciosRegistradosAsync(idEntrenador, prevDesde, prevHasta);
            var sesionesPrev = await _metricsQuery.GetSesionesRealizadasAsync(idEntrenador, prevDesde, prevHasta);

            // =======================================
            // 1) CUMPLIMIENTO GLOBAL
            // =======================================

            // actual
            var totalAct = ejerciciosActual.Count;
            var compAct = ejerciciosActual.Count(e => e.Series >= e.SeriesObjetivo);
            double cumplimientoAct = totalAct == 0 ? 0 : (double)compAct / totalAct * 100;

            // anterior
            var totalPrev = ejerciciosPrev.Count;
            var compPrev = ejerciciosPrev.Count(e => e.Series >= e.SeriesObjetivo);
            double cumplimientoPrev = totalPrev == 0 ? 0 : (double)compPrev / totalPrev * 100;

            // delta
            double deltaCumplimiento = cumplimientoPrev == 0
                ? cumplimientoAct
                : ((cumplimientoAct - cumplimientoPrev) / Math.Abs(cumplimientoPrev)) * 100;

            // =======================================
            // 2) CARGA TOTAL SEMANAL
            // =======================================

            // actual
            double cargaActual = ejerciciosActual.Sum(e => (double)e.Peso * e.Series * e.Repeticiones);

            // previa
            double cargaPrev = ejerciciosPrev.Sum(e => (double)e.Peso * e.Series * e.Repeticiones);

            // delta
            double deltaCarga = cargaPrev == 0
                ? cargaActual
                : ((cargaActual - cargaPrev) / Math.Abs(cargaPrev)) * 100;

            // =======================================
            // 3) PROGRESO FUERZA (1 RM PROMEDIO)
            // =======================================

            // actual
            double fuerzaActual = ejerciciosActual
                .Select(e => (double)(e.Peso * (1m + 0.0333m * e.Repeticiones)))
                .DefaultIfEmpty(0)
                .Average();

            // anterior
            double fuerzaPrev = ejerciciosPrev
                .Select(e => (double)(e.Peso * (1m + 0.0333m * e.Repeticiones)))
                .DefaultIfEmpty(0)
                .Average();

            // delta
            double deltaFuerza = fuerzaPrev == 0
                ? fuerzaActual
                : ((fuerzaActual - fuerzaPrev) / Math.Abs(fuerzaPrev)) * 100;


            var grafiquitos = new List<GraficoItemDto>
            {
                new()
                {
                    Title = "Cumplimiento Global",
                    Value = $"{cumplimientoAct:0}%",
                    Delta = $"{deltaCumplimiento:+0.0;-0.0;0}%",
                    Color = deltaCumplimiento >= 0 ? "#4f46e5" : "#dc2626"
                },
                new()
                {
                    Title = "Carga Total Semanal",
                    Value = $"{(cargaActual / 1000):0.0}k kg",
                    Delta = $"{deltaCarga:+0.0;-0.0;0}%",
                    Color = deltaCarga >= 0 ? "#06b6d4" : "#dc2626"
                },
                new()
                {
                    Title = "Progreso Fuerza",
                    Value = $"{fuerzaActual:0}",
                    Delta = $"{deltaFuerza:+0.0;-0.0;0}%",
                    Color = deltaFuerza >= 0 ? "#10b981" : "#dc2626"
                }
            };



            // ============================
            // 🔵 10. RETURN FINAL
            // ============================

            return new MetricaResponseDto
            {
                Grafiquitos = grafiquitos,
                StrengthData = strengthData,
                LoadData = loadData,
                ComplianceHistory = complianceHistory,
                WeeklyCompliance = weeklyCompliance,
                SessionGapData = sessionGapData,
                Prs = prs
            };
        }


        // 🔧 UTILIDAD: convertir DayOfWeek → L-M-X-J-V-S-D

        private string GetDiaSemana(DayOfWeek day)
        {
            return day switch
            {
                DayOfWeek.Monday => "L",
                DayOfWeek.Tuesday => "M",
                DayOfWeek.Wednesday => "X",
                DayOfWeek.Thursday => "J",
                DayOfWeek.Friday => "V",
                DayOfWeek.Saturday => "S",
                DayOfWeek.Sunday => "D",
                _ => "?"
            };
        }
    }
}