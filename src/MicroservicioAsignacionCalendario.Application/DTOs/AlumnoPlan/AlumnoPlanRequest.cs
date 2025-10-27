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
        [DataType(DataType.DateTime)]
        public DateTime FechaInicio { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime FechaFin { get; set; }
        [Required]
        [Range(0, 3, ErrorMessage = "El intervalo de días de descanso debe ser un número entre 0 y 3.")]
        public int IntervaloDiasDescanso { get; set; }
        public string? Notas { get; set; }
    }
}
