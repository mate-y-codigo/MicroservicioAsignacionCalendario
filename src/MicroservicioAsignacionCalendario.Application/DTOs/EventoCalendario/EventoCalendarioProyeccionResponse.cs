using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicioAsignacionCalendario.Application.DTOs.EventoCalendario
{
    public class EventoCalendarioProyeccionResponse
    {
        public DateTime Fecha { get; set; }
        public bool EsHoy { get; set; }
        public TipoDia Tipo { get; set; }
        public Guid? IdEvento { get; set; }
        public Guid? IdSesionEntrenamiento { get; set; }
        public string NombreSesion { get; set; } = string.Empty;
        public EstadoProyeccion Estado { get; set; }
    }

    public enum TipoDia { Descanso, Entrenamiento }
    public enum EstadoProyeccion { Pendiente, Proyectado }
}
