using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicioAsignacionCalendario.Application.DTOs.EjercicioSesion
{
    public class EjercicioSesionResponse
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public int SeriesObjetivo { get; set; }
        [Required]
        public int RepeticionesObjetivo { get; set; }
        [Required]
        public int PesoObjetivo { get; set; }
        [Required]
        public int Descanso { get; set; }
        [Required]
        public int Orden { get; set; }
        [Required]
        public Ejercicio Ejercicio { get; set; }
    }
}
