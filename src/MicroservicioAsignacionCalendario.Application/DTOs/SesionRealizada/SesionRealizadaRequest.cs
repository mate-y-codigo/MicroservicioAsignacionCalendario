using MicroservicioAsignacionCalendario.Application.DTOs.EjercicioRegistro;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicioAsignacionCalendario.Application.DTOs.SesionRealizada
{
    public class SesionRealizadaRequest
    {
        [Required]
        public Guid IdSesionEntrenamiento { get; set; }
        // TO DO: El IdAlumno debe venir desde authentication..
        [Required]
        public Guid IdAlumno { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime FechaRealizacion { get; set; }
        [Required]
        public List<EjercicioRegistroRequest> RegistroEjercicios { get; set; }
    }
}
