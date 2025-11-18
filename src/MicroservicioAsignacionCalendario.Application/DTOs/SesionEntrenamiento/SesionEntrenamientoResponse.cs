using MicroservicioAsignacionCalendario.Application.DTOs.EjercicioSesion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicioAsignacionCalendario.Application.DTOs.SesionEntrenamiento
{
    public class SesionEntrenamientoResponse
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Orden { get; set; }
        public List<EjercicioSesionResponse> SesionesEjercicio { get; set; }
    }
}
