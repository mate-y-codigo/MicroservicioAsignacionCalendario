using System.ComponentModel.DataAnnotations.Schema;

namespace MicroservicioAsignacionCalendario.Domain.Entities
{
    public class RecordPersonal
    {
        // Referencias
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid IdAlumno { get; set; }
        public Guid IdAlumnoPlan { get; set; }
        public Guid IdEjercicio { get; set; }
        public Guid IdSesionRealizada { get; set; }
        public Guid IdEjercicioSesion { get; set; }

        // Snapshots de Ejercicio y GrupoMuscular
        public string NombreEjercicio { get; set; }
        public string NombreGrupoMuscular { get; set; }

        // Datos del Record
        public decimal PesoMax { get; set; }
        public int Series { get; set; }
        public int Repeticiones { get; set; }
        public DateTime FechaRegistro { get; set; }

        // Metricas adicionales
        public decimal Calculo1RM { get; set; }

        // Relaciones de Base de Datos
        [ForeignKey("IdAlumnoPlan")]
        public virtual AlumnoPlan AlumnoPlan { get; set; }
        public virtual SesionRealizada SesionRealizada { get; set; }
    }
}
