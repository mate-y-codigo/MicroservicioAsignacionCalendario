using System.ComponentModel.DataAnnotations;

namespace MicroservicioAsignacionCalendario.Application.DTOs.EjercicioSesion
{
    public class Ejercicio
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string MusculoPrincipal { get; set; }
        [Required]
        public string GrupoMuscular { get; set; }
        [Required]
        public string? UrlDemo { get; set; }
        [Required]
        public bool Activo { get; set; }
        [Required]
        public Categoria Categoria { get; set; }

    }
}
