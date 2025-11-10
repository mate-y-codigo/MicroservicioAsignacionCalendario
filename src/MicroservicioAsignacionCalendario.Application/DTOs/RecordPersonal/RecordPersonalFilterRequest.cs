using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicioAsignacionCalendario.Application.DTOs.RecordPersonal
{
    public class RecordPersonalFilterRequest
    {
        [Required]
        public Guid IdAlumno { get; set; }
        public Guid? IdEjercicio { get; set; }
        public decimal? PesoMinimo { get; set; }
        public int? RepeticionesMinimas { get; set; }
        public DateTime? Desde { get; set; }
        public DateTime? Hasta{ get; set; }
        public string? OrdenarPor { get; set; } = "FechaRegistro";
        public string? Orden { get; set; } = "desc";
    }
}
