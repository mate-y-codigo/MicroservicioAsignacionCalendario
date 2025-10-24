namespace MicroservicioAsignacionCalendario.Application.Interfaces.RegistroEjercicio
{
    public interface IRegistroEjercicioService
    {
        Task<object> RegistrarEjercicioAsync(object registro);
        Task<IEnumerable<object>> ObtenerRegistrosAsync(Guid alumno_id);
    }
}
