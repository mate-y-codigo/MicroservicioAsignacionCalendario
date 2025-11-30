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
        // Referencias
        public Guid Id { get; set; }
        public Guid IdSesionEntrenamiento { get; set; }
        public Guid IdAlumnoPlan { get; set; }
        public Guid IdAlumno { get; set; }

        // Snapshots: Sesion de entrenamiento
        public string NombreSesion { get; set; }
        public int OrdenSesion { get; set; }

        // Snapshots: Alumno
        public decimal? PesoCorporalAlumno { get; set; }
        public decimal? AlturaEnCmAlumno { get; set; }

        // Otros
        public DateTime FechaRealizacion { get; set; }
        public EstadoSesion Estado { get; set; }
        public List<EjercicioRegistroResponse> EjerciciosRegistrados { get; set; }
    }
}
