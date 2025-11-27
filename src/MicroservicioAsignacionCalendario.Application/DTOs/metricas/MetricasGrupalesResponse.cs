namespace MicroservicioAsignacionCalendario.Application.DTOs.Metricas
{
    public class MetricasGrupalesResponse
    {
        public CumplimientoDiarioDto Cumplimiento { get; set; } = new();
        // Más métricas se agregarán acá luego:
        // public CargaSemanalDto CargaSemanal { get; set; }
        // public FuerzaSerieDto Fuerza { get; set; }
        // etc...
    }
}
