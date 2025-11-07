using MicroservicioAsignacionCalendario.Domain.Entities;
namespace MicroservicioAsignacionCalendario.Application.DTOs.EventoCalendario


{
    public class EventoCalendarioResponse
    {
        public Guid Id { get; set; }
        public Guid IdAlumno { get; set; }
        public Guid IdEntrenador { get; set; }
        public Guid IdSesionEntrenamiento { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public EstadoEvento Estado { get; set; }
        public string Notas { get; set; }
    }
}
