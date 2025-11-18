using MicroservicioAsignacionCalendario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicioAsignacionCalendario.Application.DTOs.SesionRealizada
{
    public class SesionRealizadaListResponse
    {
        public Guid Id { get; set; }
        public DateTime FechaRealizacion { get; set; }
        public Guid IdPlanAlumno { get; set; }
        public Guid IdSesionEntrenamiento { get; set; }
        public EstadoSesion Estado { get; set; }
        public string NombrePlan { get; set; }
        public string NombreSesion { get; set; }
    }
}
