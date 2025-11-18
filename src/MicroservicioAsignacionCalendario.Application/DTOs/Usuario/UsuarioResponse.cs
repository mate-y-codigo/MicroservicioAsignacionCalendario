using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicioAsignacionCalendario.Application.DTOs.Usuario
{
    public class UsuarioResponse
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public string Email { get; set; }
        public string? Celular { get; set; }
        [Required]
        public int RolId { get; set; }
        [Required]
        public string Rol { get; set; }
        public decimal? Peso { get; set; }
        public decimal? Altura { get; set; }
        [Required]
        public DateTime CreadoEn { get; set; }
    }
}
