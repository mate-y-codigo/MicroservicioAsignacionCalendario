namespace MicroservicioAsignacionCalendario.Domain.Entities
{
    public class SesionRealizada
    {
        public Guid Id { get; set; }
        public Guid IdSesionEntrenamiento { get; set; }
        public Guid IdPlanAlumno { get; set; } 
        public DateTimeOffset FechaRealizacion { get; set; }
        public Estado Estado { get; set; }
    }
}
