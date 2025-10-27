using System.ComponentModel.DataAnnotations;

namespace MicroservicioAsignacionCalendario.Application.DTOs.AlumnoPlan
{
    public class AlumnoPlanRequest
    {
        [Required]
        public Guid IdAlumno { get; set; }
        [Required]
        public Guid IdPlanEntrenamiento { get; set; }
        [Required]
        public DateTime FechaInicio { get; set; }
        [Required]
        public DateTime FechaFin { get; set; }
        [Required]
        public int IntervaloDiasDescanso { get; set; }
        public string? Notas { get; set; }
    }
}
