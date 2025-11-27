using MicroservicioAsignacionCalendario.Domain.Entities;

namespace MicroservicioAsignacionCalendario.Application.DTOs.EventoCalendario
{
    public class EventoCalendarioFilterRequest
    {
        public Guid? IdAlumno { get; set; }
        public Guid? IdEntrenador { get; set; }
        public DateTime? Desde { get; set; }
        public DateTime? Hasta { get; set; }
        public EstadoEvento? Estado { get; set; }
        public string? Notas { get; set; }
        public Guid? IdSesionEntrenamiento { get; set; }

    }
}
