using MicroservicioAsignacionCalendario.Application.DTOs.RecordPersonal;

namespace MicroservicioAsignacionCalendario.Application.Interfaces.RecordPersonal
{
    public interface IRecordPersonalService
    {
        Task<List<RecordPersonalResponse>> ObtenerRecordsPersonalesAsync(Guid alumno_id);
    }
}
