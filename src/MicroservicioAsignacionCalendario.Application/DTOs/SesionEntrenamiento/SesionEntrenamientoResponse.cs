using MicroservicioAsignacionCalendario.Application.DTOs.EjercicioSesion;
using System.ComponentModel.DataAnnotations;

namespace MicroservicioAsignacionCalendario.Application.DTOs.SesionEntrenamiento
{
    public class SesionEntrenamientoResponse
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid IdTrainingPlan { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Order { get; set; }
        [Required]
        public List<EjercicioSesionShortResponse> ExerciseSessions { get; set; }
    }
}
