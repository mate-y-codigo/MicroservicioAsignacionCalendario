namespace MicroservicioAsignacionCalendario.Application.DTOs.RecordPersonal
{
    public class RecordPersonalResponse
    {
        public Guid Id { get; set; }
        public Guid IdAlumno { get; set; }
        public Guid IdEjercicio { get; set; }
        public string NombreEjercicio { get; set; }
        public string NombreGrupoMuscular { get; set; }
        public Guid IdSesionRealizada { get; set; } // devolvemos la sesion en la cual se consiguio el pr
        public decimal PesoMax { get; set; }
        public int Series { get; set; }
        public int Repeticiones { get; set; }
        public DateTime FechaRegistro { get; set; }
        public decimal Calculo1RM { get; set; }
    }
}
