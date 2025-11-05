namespace MicroservicioAsignacionCalendario.Application.DTOs.EjercicioRegistro
{
    public class EjercicioRegistroFilterRequest
    {
        public Guid? IdAlumno { get; set; }
        public Guid? IdSesionEntrenamiento { get; set; }
        public Guid? IdEjercicio { get; set; }
    }
}
