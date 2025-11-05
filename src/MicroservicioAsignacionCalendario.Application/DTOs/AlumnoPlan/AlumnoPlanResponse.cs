using MicroservicioAsignacionCalendario.Domain.Entities;

namespace Application.DTOs.AlumnoPlan
{
    public class AlumnoPlanResponse
    {
        public Guid Id { get; set; }
        public Guid IdAlumno { get; set; }
        public Guid IdPlanEntrenamiento { get; set; }
        public Guid IdSesionActual { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int IntervaloDiasDescanso { get; set; }
        public EstadoAlumnoPlan Estado { get; set; }
        public string? Notas { get; set; }
    }
}
