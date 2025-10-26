using MicroservicioAsignacionCalendario.Application.DTOs.SesionEntrenamiento;
using System.ComponentModel.DataAnnotations;

namespace MicroservicioAsignacionCalendario.Application.DTOs.PlanEntrenamiento
{
    public class PlanEntrenamientoResponse
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool IsTemplate { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public DateTime UpdateDate { get; set; }
        [Required]
        public bool Active { get; set; }
        [Required]
        public List<SesionEntrenamientoResponse> TrainingSessions { get; set; }
    }
}
