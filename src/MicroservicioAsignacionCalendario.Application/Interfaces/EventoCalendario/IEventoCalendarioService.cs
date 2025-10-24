namespace MicroservicioAsignacionCalendario.Application.Interfaces.EventoCalendario
{
    public interface IEventoCalendarioService
    {
        Task<object> ObtenerEventosAsync(DateTime? desde, DateTime? hasta, string? alumnoId);
    }
}
