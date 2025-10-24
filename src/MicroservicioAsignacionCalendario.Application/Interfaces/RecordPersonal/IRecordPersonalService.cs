namespace MicroservicioAsignacionCalendario.Application.Interfaces.RecordPersonal
{
    public interface IRecordPersonalService
    {
        Task<object> ObtenerRecordPersonalAsync(Guid alumno_id);
    }
}
