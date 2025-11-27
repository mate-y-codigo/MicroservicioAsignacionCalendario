using MicroservicioAsignacionCalendario.Application.DTOs.Metricas;

namespace MicroservicioAsignacionCalendario.Application.Interfaces.Metricas
{
    public interface IMetricasService
    {
        Task<MetricasGrupalesResponse> ObtenerMetricasGrupalesAsync(
            Guid idEntrenador,
            DateTime desde,
            DateTime hasta);
    }
}
