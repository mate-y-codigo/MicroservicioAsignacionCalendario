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
        public DateTimeOffset FechaInicio { get; set; }
        [Required]
        public DateTimeOffset FechaFin { get; set; }
        public string? Notas { get; set; }
    }
}
