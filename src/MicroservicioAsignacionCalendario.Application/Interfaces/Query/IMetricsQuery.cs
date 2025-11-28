

using MicroservicioAsignacionCalendario.Domain.Entities;

namespace Interfaces.Query
{
    public interface IMetricsQuery
    {
        Task<List<AlumnoPlan>> GetPlanesDelEntrenadorAsync(Guid idEntrenador);

        Task<List<SesionRealizada>> GetSesionesRealizadasAsync(Guid idEntrenador, DateTime desde, DateTime hasta);

        Task<List<EjercicioRegistro>> GetEjerciciosRegistradosAsync(Guid idEntrenador, DateTime desde, DateTime hasta);

        Task<List<EventoCalendario>> GetEventosProgramadosAsync(Guid idEntrenador, DateTime desde, DateTime hasta);

        Task<List<RecordPersonal>> GetRecordsPersonalesAsync(Guid idEntrenador, DateTime desde, DateTime hasta);
    }
}
