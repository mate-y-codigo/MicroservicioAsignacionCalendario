using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicioAsignacionCalendario.Domain.Entities
{
    public class EjercicioRegistro
    {
        Guid Id { get; set; }
        Guid IdSesionRealizada { get; set; }
        Guid IdEjercicio { get; set; }
        int Series { get; set; }
        int Repeticiones { get; set; }
        decimal Peso { get; set; }
        bool Completado { get; set; }
    }
}
