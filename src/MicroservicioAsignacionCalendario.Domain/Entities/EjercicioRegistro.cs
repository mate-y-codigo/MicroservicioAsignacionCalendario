using System.ComponentModel.DataAnnotations.Schema;

namespace MicroservicioAsignacionCalendario.Domain.Entities
{
    public class EjercicioRegistro
    {
        // Referencias
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid IdSesionRealizada { get; set; }
        public Guid IdEjercicio { get; set; }
        public Guid IdEjercicioSesion {  get; set; }

        // Snapshots: Ejercicio sesion
        public int SeriesObjetivo { get; set; }
        public int RepeticionesObjetivo { get; set; }
        public decimal PesoObjetivo { get; set; }
        public int DescansoObjetivo { get; set; }
        public int OrdenEjercicio { get; set; }

        // Snapshots: Ejercicio
        public string NombreEjercicio { get; set; } = string.Empty;
        public string NombreMusculo { get; set; } = string.Empty;
        public string NombreGrupoMuscular { get; set; } = string.Empty;
        public string NombreCategoria { get; set; } = string.Empty;
        public string UrlDemoEjercicio { get; set; } = string.Empty;

        // Datos Reales
        public int Series { get; set; }
        public int Repeticiones { get; set; }
        public decimal Peso { get; set; }
        public DateTime FechaRealizacion { get; set; }

        // Relacion Base de datos
        public virtual SesionRealizada SesionRealizada { get; set; } = null!;

        // Valor autocalculado
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
