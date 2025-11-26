namespace MicroservicioAsignacionCalendario.Domain.Entities
{
    public class EventoCalendario
    {
        // Referencias
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid IdAlumnoPlan { get; set; }
        public Guid IdSesionEntrenamiento { get; set; }

        // Snapshots: Sesion de entrenamiento
        public string NombreSesion { get; set; } = string.Empty;

        // Otros
        public EstadoEvento Estado { get; set; } = EstadoEvento.Programado;
        public string? Notas { get; set; }
        public DateTime FechaProgramada { get; set; }

        // Relaciones Base de datos
        public virtual AlumnoPlan AlumnoPlan { get; set; } = null!;
    }
}
