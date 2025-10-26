using System.ComponentModel.DataAnnotations;

namespace MicroservicioAsignacionCalendario.Application.DTOs.EjercicioSesion
{
    public class Categoria
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Nombre { get; set; }
    }
}
