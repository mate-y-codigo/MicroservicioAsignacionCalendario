using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicioAsignacionCalendario.Application.DTOs.SesionRealizada
{
    public class SesionRealizadaFilterRequest
    {
        [Required]
        public Guid IdEntrenador { get; set; }
        public Guid? IdAlumno { get; set; }
        public Guid? IdPlanEntrenamiento { get; set; }
        public Guid? IdSesionEntrenamiento { get; set; }
        public DateTime? Desde { get; set; }
        public DateTime? Hasta { get; set; }
    }
}
