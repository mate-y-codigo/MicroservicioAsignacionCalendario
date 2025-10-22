using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicioAsignacionCalendario.Domain.Entities
{
    public class EjercicioRegistro
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid IdSesionRealizada { get; set; }
        public Guid IdEjercicio { get; set; }
        public int Series { get; set; }
        public int Repeticiones { get; set; }
        public decimal Peso { get; set; }
        public bool Completado { get; set; }

        public SesionRealizada SesionRealizada = null!;
    }
}
