using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicioAsignacionCalendario.Application.DTOs.AlumnoPlan
{
    public class AsignarPlanRequest
    {
        [Required] public Guid IdAlumno { get; set; }
        [Required] public Guid IdPlanEntrenamiento { get; set; }
        [Required] public Guid IdEntrenador { get; set; }
        [Required] public DateOnly FechaInicio { get; set; }
        public DateOnly? FechaFin { get; set; }
        public bool OverridePlanActivo { get; set; } = true;
        public string? Notas { get; set; }
    }
}
