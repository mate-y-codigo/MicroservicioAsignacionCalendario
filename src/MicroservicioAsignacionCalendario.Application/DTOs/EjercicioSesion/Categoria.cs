using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
