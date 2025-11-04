using System.ComponentModel.DataAnnotations;

namespace MicroservicioAsignacionCalendario.Application.DTOs.EjercicioSesion
{
    public class EjercicioSesionShortResponse
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid IdEjercicio { get; set; }
        [Required]
        public string NombreEjercicio { get; set; }
        [Required]
        public int SeriesObjetivo { get; set; }
        [Required]
        public int RepeticionesObjetivo { get; set; }
        [Required]
        public float PesoObjetivo { get; set; }
    }
}
