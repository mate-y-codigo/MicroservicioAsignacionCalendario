namespace MicroservicioAsignacionCalendario.Domain.Entities
{
    public class EventoCalendario
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid IdAlumno { get; set; }
        public Guid IdEntrenador { get; set; }
        public Guid IdSesionEntrenamiento { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public EstadoEvento Estado { get; set; } = EstadoEvento.Programado;
        public string? Notas { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
