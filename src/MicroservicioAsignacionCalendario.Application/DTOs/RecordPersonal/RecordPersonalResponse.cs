namespace MicroservicioAsignacionCalendario.Application.DTOs.RecordPersonal
{
    public class RecordPersonalResponse
    {
        public Guid Id { get; set; }
        public Guid IdAlumno { get; set; }
        public Guid IdEjercicio { get; set; }
        public decimal PesoMax { get; set; }
        public int Series { get; set; }
        public int Repeticiones { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
