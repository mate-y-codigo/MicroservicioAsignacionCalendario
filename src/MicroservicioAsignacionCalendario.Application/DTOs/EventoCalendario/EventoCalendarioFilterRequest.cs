namespace MicroservicioAsignacionCalendario.Application.DTOs.EventoCalendario
{
    public class EventoCalendarioFilterRequest
    {
        public Guid? IdAlumno { get; set; }
        public DateTime? Desde { get; set; }
        public DateTime? Hasta { get; set; }
    }
}
