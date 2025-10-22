namespace MicroservicioAsignacionCalendario.Domain.Entities
{
    public class EventoCalendario
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid IdAlumno { get; set; }
        public Guid IdEntrenador { get; set; }
        public Guid IdSesionEntrenamiento { get; set; }
        public DateTimeOffset FechaInicio { get; set; }
        public DateTimeOffset FechaFin { get; set; }
        public Estado Estado { get; set; }
        public string? Notas { get; set; }
        public DateTimeOffset FechaCreacion { get; set; }
        public DateTimeOffset FechaActualizacion { get; set; }
    }
}
