using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicioAsignacionCalendario.Domain.Entities
{
    public class AlumnoPlan
    {
        Guid IdAlumno { get; set; }
        Guid IdPlanEntrenamiento { get; set; }
        Guid IdSesionActual { get; set; }
        DateTimeOffset FechaInicio { get; set; }
        DateTimeOffset FechaFin { get; set; }
        Estado Estado { get; set; }
        string? Notas { get; set; }

    }
}
