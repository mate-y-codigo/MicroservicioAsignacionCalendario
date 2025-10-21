namespace MicroservicioAsignacionCalendario.Domain.Entities
{
    public class EventoCalendario
    {
        Guid Id { get; set; }
        Guid IdAlumno { get; set; }
        Guid IdEntrenador { get; set; }
        Guid IdSesionEntrenamiento { get; set; }
        DateTimeOffset FechaInicio { get; set; }
        DateTimeOffset FechaFin { get; set; }
        Estado Estado { get; set; }
        string? Notas { get; set; }
        DateTimeOffset FechaCreacion { get; set; }
        DateTimeOffset FechaActualizacion { get; set; }
    }
}
