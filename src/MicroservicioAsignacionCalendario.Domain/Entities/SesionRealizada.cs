namespace MicroservicioAsignacionCalendario.Domain.Entities
{
    public class SesionRealizada
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid IdSesionEntrenamiento { get; set; }
        public Guid IdPlanAlumno { get; set; }
        public DateTime FechaRealizacion { get; set; }
        public EstadoSesion Estado { get; set; } = EstadoSesion.Pendiente;
        public virtual AlumnoPlan AlumnoPlan { get; set; } = null!;
        public ICollection<EjercicioRegistro> EjerciciosRegistrados { get; set; } = new List<EjercicioRegistro>();
    }
}
