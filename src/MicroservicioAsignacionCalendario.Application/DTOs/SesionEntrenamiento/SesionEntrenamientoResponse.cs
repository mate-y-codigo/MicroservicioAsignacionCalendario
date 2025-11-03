using MicroservicioAsignacionCalendario.Application.DTOs.EjercicioSesion;
using System.ComponentModel.DataAnnotations;

namespace MicroservicioAsignacionCalendario.Application.DTOs.SesionEntrenamiento
{
    public class SesionEntrenamientoResponse
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid PlanEntrenamientoId { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public int Orden { get; set; }
        [Required]
        public List<EjercicioSesionShortResponse> SesionesEjercicio { get; set; }
    }
}
