using MicroservicioAsignacionCalendario.Application.DTOs.EventoCalendario;
using MicroservicioAsignacionCalendario.Application.DTOs.SesionRealizada;
using MicroservicioAsignacionCalendario.Domain.Entities;

namespace Application.DTOs.AlumnoPlan
{
    public class AlumnoPlanResponse
    {
        // Referencias
        public Guid Id { get; set; }
        public Guid IdAlumno { get; set; }
        public Guid IdPlanEntrenamiento { get; set; }
        public Guid IdSesionARealizar { get; set; }

        // Plan Entrenamiento
        public string NombrePlan { get; set; }
        public string DescripcionPlan { get; set; }

        // Otros
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int IntervaloDiasDescanso { get; set; }
        public EstadoAlumnoPlan Estado { get; set; }
        public string Notas { get; set; }
        
        // Relaciones
        public ICollection<SesionRealizadaResponse> SesionesRealizadas { get; set; } = new List<SesionRealizadaResponse>();
        public ICollection<EventoCalendarioResponse> EventosCalendarios { get; set; } = new List<EventoCalendarioResponse>();
    }
}
