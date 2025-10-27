namespace MicroservicioAsignacionCalendario.Domain.Entities
{
    public class SesionRealizada
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid IdSesionEntrenamiento { get; set; }
        public Guid IdPlanAlumno { get; set; }
        public DateTime FechaRealizacion { get; set; }
        public Estado Estado { get; set; } = Estado.Activo;
        public AlumnoPlan AlumnoPlan { get; set; } = null!;
        public ICollection<EjercicioRegistro> EjerciciosRegistrados { get; set; } = new List<EjercicioRegistro>();
    }
}
