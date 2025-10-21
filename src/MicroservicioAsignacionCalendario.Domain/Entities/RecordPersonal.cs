namespace MicroservicioAsignacionCalendario.Domain.Entities
{
    public class RecordPersonal
    {
        Guid Id { get; set; }
        Guid IdAlumno { get; set; }
        Guid IdEjercicio { get; set; }
        decimal PesoMax { get; set; }
        int Series { get; set; }
        int Repeticiones { get; set; }
        DateTimeOffset FechaRegistro { get; set; }
    }
}
