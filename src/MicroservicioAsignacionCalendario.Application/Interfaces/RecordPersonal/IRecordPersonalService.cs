using MicroservicioAsignacionCalendario.Application.DTOs.RecordPersonal;
using MicroservicioAsignacionCalendario.Domain.Entities;

namespace MicroservicioAsignacionCalendario.Application.Interfaces.RecordPersonal
{
    public interface IRecordPersonalService
    {
        Task ActualizarRecordPersonalAsync(EjercicioRegistro registro);
        Task<List<RecordPersonalResponse>> ObtenerRecordsPersonalesAsync(RecordPersonalFilterRequest filtros);
    }
}
