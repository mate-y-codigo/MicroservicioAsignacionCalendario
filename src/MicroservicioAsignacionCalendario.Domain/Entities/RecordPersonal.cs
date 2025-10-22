namespace MicroservicioAsignacionCalendario.Domain.Entities
{
    public class RecordPersonal
    {
        public Guid Id { get; set; } = new Guid();
        public Guid IdAlumno { get; set; }
        public Guid IdEjercicio { get; set; }
        public decimal PesoMax { get; set; }
        public int Series { get; set; }
        public int Repeticiones { get; set; }
        public DateTimeOffset FechaRegistro { get; set; }
    }
}
