using MicroservicioAsignacionCalendario.Application.DTOs.RecordPersonal;
using MicroservicioAsignacionCalendario.Application.Interfaces.RecordPersonal;

namespace MicroservicioAsignacionCalendario.Application.Services
{
    public class RecordPersonalService : IRecordPersonalService
    {
        // TO DO: Agregar CQRS
        public RecordPersonalService() { }

        // TO DO: Implementar método
        public async Task<List<RecordPersonalResponse>> ObtenerRecordsPersonalesAsync(Guid alumno_id)
        {
            throw new NotImplementedException();
        }
    }
}
