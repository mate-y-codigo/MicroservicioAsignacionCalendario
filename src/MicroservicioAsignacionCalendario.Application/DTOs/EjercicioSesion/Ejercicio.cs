using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
