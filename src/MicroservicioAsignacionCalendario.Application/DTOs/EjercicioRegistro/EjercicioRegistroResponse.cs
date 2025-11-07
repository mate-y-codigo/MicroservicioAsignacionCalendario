namespace MicroservicioAsignacionCalendario.Application.DTOs.EjercicioRegistro
{
    public class EjercicioRegistroResponse
    {
        public Guid Id { get; set; }
        // TO DO: incluir los datos de la sesion?
        public Guid IdSesionRealizada { get; set; }
        // TO DO: incluir los datos del ejercicio?
        public Guid IdEjercicio { get; set; }
        public int Series { get; set; }
        public int SeriesObjetivo { get; set; }
        public int Repeticiones { get; set; }
        public int RepeticionesObjetivo { get; set; }
        public decimal Peso { get; set; }
        public decimal PesoObjetivo { get; set; }
        public bool Completado { get; set; }
    }
}
