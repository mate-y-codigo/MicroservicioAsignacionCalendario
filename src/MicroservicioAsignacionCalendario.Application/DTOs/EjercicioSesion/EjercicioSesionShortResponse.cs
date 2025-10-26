using System.ComponentModel.DataAnnotations;

namespace MicroservicioAsignacionCalendario.Application.DTOs.EjercicioSesion
{
    public class EjercicioSesionShortResponse
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid EjercicioId { get; set; }
    }
}
