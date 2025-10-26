namespace MicroservicioAsignacionCalendario.Domain.Entities
{
    public class AlumnoPlan
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid IdAlumno { get; set; }
        public Guid IdPlanEntrenamiento { get; set; }
        public Guid IdSesionActual { get; set; }
        public DateTimeOffset FechaInicio { get; set; }
        public DateTimeOffset FechaFin { get; set; }
        public int IntervaloDiasDescanso { get; set; }
        public Estado Estado { get; set; }
        public string? Notas { get; set; }
        public ICollection<SesionRealizada> SesionesRealizadas { get; set; } = new List<SesionRealizada>();
    }
}
