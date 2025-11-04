using System.ComponentModel.DataAnnotations.Schema;

namespace MicroservicioAsignacionCalendario.Domain.Entities
{
    public class EjercicioRegistro
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid IdSesionRealizada { get; set; }
        public Guid IdEjercicio { get; set; }
        public int Series { get; set; }
        public int SeriesObjetivo { get; set; }
        public int Repeticiones { get; set; }
        public int RepeticionesObjetivo { get; set; }
        public decimal Peso { get; set; }
        public decimal PesoObjetivo { get; set; }
        public virtual SesionRealizada SesionRealizada { get; set; } = null!;
        [NotMapped]
        public bool Completado
        {
            get
            {
                return (Peso >= PesoObjetivo) &&
                       (Repeticiones >= RepeticionesObjetivo) &&
                       (Series >= SeriesObjetivo);
            }
        }
    }
}
