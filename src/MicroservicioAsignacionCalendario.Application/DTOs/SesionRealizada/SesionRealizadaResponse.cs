using MicroservicioAsignacionCalendario.Application.DTOs.EjercicioRegistro;
using MicroservicioAsignacionCalendario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicioAsignacionCalendario.Application.DTOs.SesionRealizada
{
    public class SesionRealizadaResponse
    {
        public Guid Id { get; set; }
        public Guid IdSesionEntrenamiento { get; set; }
        public Guid IdPlanAlumno { get; set; }
        public DateTime FechaRealizacion { get; set; }
        public EstadoSesion Estado { get; set; }
        public string NombreSesion { get; set; }
        public int OrdenSesion { get; set; }
        public decimal? PesoCorporalAlumno { get; set; }
        public decimal? AlturaEnCmAlumno { get; set; }
        public List<EjercicioRegistroResponse> EjerciciosRegistrados { get; set; }
    }
}
