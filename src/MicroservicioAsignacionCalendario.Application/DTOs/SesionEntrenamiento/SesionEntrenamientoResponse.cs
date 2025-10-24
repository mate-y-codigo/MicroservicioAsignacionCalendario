using MicroservicioAsignacionCalendario.Application.DTOs.EjercicioSesion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicioAsignacionCalendario.Application.DTOs.SesionEntrenamiento
{
    public class SesionEntrenamientoResponse
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid IdPlanEntrenamiento { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public int Orden { get; set; }
        [Required]
        public List<EjercicioSesionShortResponse> EjerciciosSesion { get; set; }
    }
}
