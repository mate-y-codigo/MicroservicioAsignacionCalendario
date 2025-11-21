namespace MicroservicioAsignacionCalendario.Domain.Entities
{
    public class AlumnoPlan
    {
        // Referencias
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid IdAlumno { get; set; }
        public Guid IdPlanEntrenamiento { get; set; }
        public Guid IdSesionARealizar { get; set; }

        // Snapshots: Plan de Entrenamiento
        public string NombrePlan { get; set; } = string.Empty;
        public string DescripcionPlan { get; set; } = string.Empty;

        // Otros
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int IntervaloDiasDescanso { get; set; }
        public EstadoAlumnoPlan Estado { get; set; } = EstadoAlumnoPlan.Activo;
        public string? Notas { get; set; }

        // Relación Base de datos
        public ICollection<SesionRealizada> SesionesRealizadas { get; set; } = new List<SesionRealizada>();
        public ICollection<EventoCalendario> EventosCalendarios { get; set; } = new List<EventoCalendario>();
        public ICollection<RecordPersonal> RecordsPersonales{ get; set; } = new List<RecordPersonal>();
    }
}
