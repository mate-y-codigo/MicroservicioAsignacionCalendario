using System.ComponentModel.DataAnnotations;

namespace MicroservicioAsignacionCalendario.Application.DTOs.EjercicioSesion
{
    public class EjercicioResponse
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public MusculoResponse Musculo { get; set; }
        [Required]
        public string? UrlDemo { get; set; }
        [Required]
        public bool Activo { get; set; }
        [Required]
        public Categoria Categoria { get; set; }
    }

    public class MusculoResponse
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public GrupoMuscular GrupoMuscular { get; set; }
    }

    public class GrupoMuscular
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
    }

    public class Categoria
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
    }
}
