using System.ComponentModel.DataAnnotations;

namespace MicroservicioAsignacionCalendario.Application.DTOs.AlumnoPlan
{
    public class AlumnoPlanRequest
    {
        [Required]
        public Guid IdAlumno { get; set; }
        public Guid? IdEntrenador { get; set; }
        public string? NombreEntrenador { get; set; }
        [Required]
        public Guid IdPlanEntrenamiento { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime FechaInicio { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime FechaFin { get; set; }
        [Required]
        public int IntervaloDiasDescanso { get; set; }
        public string? Notas { get; set; }
    }
}
