using MicroservicioAsignacionCalendario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicioAsignacionCalendario.Application.DTOs.AlumnoPlan
{
    public class AlumnoPlanFilterRequest
    {
        public Guid? IdAlumno { get; set; }
        public Guid? IdEntrenador { get; set; }
        public Guid? IdPlanEntrenamiento { get; set; }
        public DateTime? Desde { get; set; }
        public DateTime? Hasta { get; set; }
        public EstadoAlumnoPlan? Estado { get; set; } = EstadoAlumnoPlan.Activo;
    }
}
