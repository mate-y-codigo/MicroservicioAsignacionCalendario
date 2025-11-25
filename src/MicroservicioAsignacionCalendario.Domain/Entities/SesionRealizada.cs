namespace MicroservicioAsignacionCalendario.Domain.Entities
{
    public class SesionRealizada
    {
        // Referencias
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid IdSesionEntrenamiento { get; set; }
        public Guid IdAlumnoPlan { get; set; }

        // Snapshots: Sesion de entrenamiento
        public string NombreSesion { get; set; } = string.Empty;
        public int OrdenSesion { get; set; }

        // Snapshots: Alumno
        public decimal? PesoCorporalAlumno { get; set; }
        public decimal? AlturaEnCmAlumno { get; set; }

        // Otros
        public DateTime? FechaRealizacion { get; set; }
        public EstadoSesion Estado { get; set; } = EstadoSesion.Pendiente;

        // Relaciones de Base de datos
        public virtual AlumnoPlan AlumnoPlan { get; set; } = null!;
        public ICollection<EjercicioRegistro> EjerciciosRegistrados { get; set; } = new List<EjercicioRegistro>();
    }
}
