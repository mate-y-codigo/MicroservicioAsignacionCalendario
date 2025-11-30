using System.Globalization;
using MicroservicioAsignacionCalendario.Application.DTOs.metricas;
using MicroservicioAsignacionCalendario.Application.Interfaces.Query;
using MicroservicioAsignacionCalendario.Domain.Entities;



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

            return GetMetricasGrupalesAsync(idAlumno, desde, hasta);
        }

        public async Task<MetricaResponseDto> GetMetricasGrupalesAsync(
            Guid idEntrenador, DateTime desde, DateTime hasta)
        {
            // ============================
            //  1. Obtener datos reales
            // ============================
            var planes = await _metricsQuery.GetPlanesDelEntrenadorAsync(idEntrenador);
            var sesiones = await _metricsQuery.GetSesionesRealizadasAsync(idEntrenador, desde, hasta);
            var ejercicios = await _metricsQuery.GetEjerciciosRegistradosAsync(idEntrenador, desde, hasta);
            var eventos = await _metricsQuery.GetEventosProgramadosAsync(idEntrenador, desde, hasta);
            var records = await _metricsQuery.GetRecordsPersonalesAsync(idEntrenador, desde, hasta);

            // ============================
            //  2. Cumplimiento global
            // ============================
            var prog = ejercicios.Count;
            var comp = ejercicios.Count(e => e.Series >= e.SeriesObjetivo);
            double cumplimientoGlobal = prog == 0 ? 0 : (double)comp / prog * 100;

            // ============================
            //  3. Carga semanal (kg totales)
            // ============================
            var loadData = ejercicios
                .GroupBy(e => GetSemanaRelativa(e.FechaRealizacion, desde))
                .Select(g => new LoadDataDto
                {
                    Week = $"Sem {g.Key}",
                    Load = g.Sum(e => (double)e.Peso * e.Repeticiones * e.Series)
                })
                .ToList();

            // ============================
            //  4. Fuerza (1RM promedio)
            // ============================
            var strengthData = ejercicios
                .Select(e => new
                {
                    week = GetSemanaRelativa(e.FechaRealizacion, desde),
                    rm = (double)(e.Peso * (1m + 0.0333m * e.Repeticiones))
                })
                .GroupBy(g => g.week)
                .Select(g => new StrengthDataDto
                {
                    Week = $"Sem {g.Key}",
                    Avg1RM = g.Average(x => x.rm)
                })
                .ToList();


            // 5. Compliance History

            var complianceHistory = eventos
                .GroupBy(e => GetSemanaRelativa(e.FechaProgramada, desde))
                .Select(g =>
                {
                    int semana = g.Key;
                    var sesionesProgramadas = g.ToList();

                    // Sesiones realizadas correspondientes
                    var sesionesRealizadasSemana = sesiones
                        .Where(s => sesionesProgramadas.Any(ev =>
                            ev.IdAlumnoPlan == s.IdAlumnoPlan &&
                            ev.IdSesionEntrenamiento == s.IdSesionEntrenamiento))
                        .ToList();

                    int totalProgramadas = sesionesProgramadas.Count;

                    int totalCumplidas = sesionesProgramadas.Count(ev =>
                        SesionCumplida(ev, sesionesRealizadasSemana, ejercicios));

                    return new ComplianceHistoryDto
                    {
                        Date = $"Sem {semana}",
                        Compliance = totalProgramadas == 0
                            ? 0
                            : (double)totalCumplidas / totalProgramadas * 100
                    };
                })
                .OrderBy(x => x.Date)
                .ToList();





            //  6. Weekly Compliance (L-M-X-J-V-S-D)
            DateTime inicioSemana = desde.Date;
            DateTime finSemana = inicioSemana.AddDays(6); // siempre 7 días exactos

            var eventosSemana = eventos.Where(e => e.FechaProgramada.Date >= inicioSemana &&
                        e.FechaProgramada.Date <= finSemana).ToList();

            var sesionesSemana = sesiones.Where(s => s.FechaRealizacion.HasValue &&
                            s.FechaRealizacion.Value.Date >= inicioSemana &&
                            s.FechaRealizacion.Value.Date <= finSemana).ToList();


            var weeklyCompliance = Enumerable.Range(0, 7)
                .Select(offset =>
                {
                    DateTime dia = inicioSemana.AddDays(offset);
                    DayOfWeek dow = dia.DayOfWeek;
                    string diaLetras = GetDiaSemana(dow);

                    // Programadas ese día
                    var programadas = eventosSemana
                        .Where(e => e.FechaProgramada.Date == dia.Date)
                        .ToList();

                    // Realizadas ese día (solo por fecha)
                    var realizadas = sesionesSemana
                        .Where(s => s.FechaRealizacion.Value.Date == dia.Date)
                        .ToList();

                    int totalProgramadas = programadas.Count;

                    int totalCumplidas = programadas.Count(ev =>
                        SesionCumplida(ev, realizadas, ejercicios)
                    );

                    return new WeeklyComplianceDto
                    {
                        Day = diaLetras,
                        Value = totalProgramadas == 0
                            ? 0
                            : (double)totalCumplidas / totalProgramadas * 100
                    };
                })
                .ToList();





            //  7. Session Gap (días entre sesiones)

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
                    Week = $"Sem {GetSemanaRelativa(curr, desde)}",
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

            // PERIODOS

            var prevDesde = desde.AddMonths(-3);
            var prevHasta = desde;


            // DATOS: ACTUAL

            var ejerciciosActual = ejercicios;
            var sesionesActual = sesiones;


            // DATOS: TRIMESTRE ANTERIOR

            var ejerciciosPrev = await _metricsQuery.GetEjerciciosRegistradosAsync(idEntrenador, prevDesde, prevHasta);
            var sesionesPrev = await _metricsQuery.GetSesionesRealizadasAsync(idEntrenador, prevDesde, prevHasta);
            var eventosPrev = await _metricsQuery.GetEventosProgramadosAsync(idEntrenador, prevDesde, prevHasta);

            // 1) CUMPLIMIENTO GLOBAL


            // actual
            int totalProgramadasAct = eventos.Count;
            int totalCumplidasAct = eventos.Count(ev => SesionCumplida(ev, sesiones, ejerciciosActual));
            double cumplimientoAct = totalProgramadasAct == 0 ? 0 : (double)totalCumplidasAct / totalProgramadasAct * 100;

            // anterior
            int totalProgramadasPrev = eventosPrev.Count;
            int totalCumplidasPrev = eventosPrev.Count(ev => SesionCumplida(ev, sesionesPrev, ejerciciosPrev));

            double cumplimientoPrev = totalProgramadasPrev == 0 ? 0 : (double)totalCumplidasPrev / totalProgramadasPrev * 100;


            // delta
            double deltaCumplimiento = cumplimientoPrev == 0 ? cumplimientoAct : ((cumplimientoAct - cumplimientoPrev) / Math.Abs(cumplimientoPrev)) * 100;


            // 2) CARGA TOTAL SEMANAL
            // actual
            double cargaActual = ejerciciosActual.Sum(e => (double)e.Peso * e.Series * e.Repeticiones);

            // previa
            double cargaPrev = ejerciciosPrev.Sum(e => (double)e.Peso * e.Series * e.Repeticiones);

            // delta
            double deltaCarga = cargaPrev == 0
                ? cargaActual
                : ((cargaActual - cargaPrev) / Math.Abs(cargaPrev)) * 100;


            // 3) PROGRESO FUERZA (1 RM PROMEDIO)


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

        private int GetSemanaRelativa(DateTime fecha, DateTime desde)
        {
            var dias = (fecha.Date - desde.Date).TotalDays;
            int semana = (int)(dias / 7); // floor automático
            return semana + 1; // Semana 1, 2, 3...
        }



        //  convertir DayOfWeek → L-M-X-J-V-S-D

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

        private bool SesionCumplida(
            EventoCalendario evento,
            List<SesionRealizada> sesionesRealizadas,
            List<EjercicioRegistro> ejercicios)
        {
            // Buscar la sesión realizada correspondiente por AlumnoPlan + SesionEntrenamiento
            var sesReal = sesionesRealizadas.FirstOrDefault(s =>
                s.IdAlumnoPlan == evento.IdAlumnoPlan &&
                s.IdSesionEntrenamiento == evento.IdSesionEntrenamiento);

            if (sesReal == null)
                return false;

            // Obtener ejercicios registrados en esa sesión
            var ej = ejercicios
                .Where(x => x.IdSesionRealizada == sesReal.Id)
                .ToList();

            if (!ej.Any())
                return false;

            // Cumplimiento por ejercicio
            return ej.All(x =>
                x.Series >= x.SeriesObjetivo &&
                x.Repeticiones >= x.RepeticionesObjetivo
            );
        }
    }
}

