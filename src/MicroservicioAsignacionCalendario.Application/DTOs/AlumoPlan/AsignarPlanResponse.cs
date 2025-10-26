using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicioAsignacionCalendario.Application.DTOs.AlumnoPlan
{
    public class AsignarPlanResponse
    {
        public Guid IdAlumnoPlan { get; set; }
        public int EventosGenerados { get; set; }
        public bool PisoPlanAnterior { get; set; }
    }
}
