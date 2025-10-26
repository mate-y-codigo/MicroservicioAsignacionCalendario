namespace MicroservicioAsignacionCalendario.Application.DTOs.EjercicioRegistro
{
    public class EjercicioRegistroFilterRequest
    {
        public Guid? IdSesionEntrenamiento { get; set; }
        public Guid? IdEjercicio { get; set; }
    }
}
