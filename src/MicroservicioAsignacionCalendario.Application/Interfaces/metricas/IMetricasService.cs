using MicroservicioAsignacionCalendario.Application.DTOs.metricas;

namespace MicroservicioAsignacionCalendario.Application.Interfaces.Query
{
    public interface IMetricsService
    {
        Task<MetricaResponseDto> GetMetricasGrupalesAsync(Guid idEntrenador, DateTime desde, DateTime hasta);
        Task<MetricaResponseDto> GetMetricasAlumnoAsync(Guid idAlumno, DateTime desde, DateTime hasta);
    }

}
