using System.ComponentModel.DataAnnotations;

namespace MicroservicioAsignacionCalendario.Application.DTOs.EjercicioRegistro
{
    public class EjercicioRegistroRequest
    {
        [Required]
        public Guid IdEjercicio { get; set; }
        [Required]
        public int Series { get; set; }
        [Required]
        public int Repeticiones { get; set; }
        [Required]
        public decimal Peso { get; set; }
    }
}
