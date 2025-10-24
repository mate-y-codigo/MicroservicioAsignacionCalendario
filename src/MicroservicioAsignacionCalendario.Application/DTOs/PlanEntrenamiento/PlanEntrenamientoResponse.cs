using MicroservicioAsignacionCalendario.Application.DTOs.SesionEntrenamiento;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicioAsignacionCalendario.Application.DTOs.PlanEntrenamiento
{
    public class PlanEntrenamientoResponse
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public bool EsPlantilla { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
        [Required]
        public DateTime FechaActualizacion { get; set; }
        [Required]
        public bool Activo { get; set; }
        [Required]
        public List<SesionEntrenamientoResponse> SesionesEntrenamiento { get; set; }
    }
}
