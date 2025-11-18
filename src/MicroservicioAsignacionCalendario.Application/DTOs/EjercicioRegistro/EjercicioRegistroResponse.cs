namespace MicroservicioAsignacionCalendario.Application.DTOs.EjercicioRegistro
{
    public class EjercicioRegistroResponse
    {
        public Guid Id { get; set; }
        public Guid IdSesionRealizada { get; set; }
        public Guid IdEjercicio { get; set; }
        public Guid IdEjercicioSesion { get; set; }
        public string NombreEjercicio { get; set; }
        public string NombreMusculo { get; set; }
        public string NombreGrupoMuscular { get; set; }
        public string NombreCategoria { get; set; }
        public string UrlDemoEjercicio { get; set; }
        public int OrdenEjercicio { get; set; }
        public int DescansoObjetivo { get; set; }
        public int Series { get; set; }
        public int SeriesObjetivo { get; set; }
        public int Repeticiones { get; set; }
        public int RepeticionesObjetivo { get; set; }
        public decimal Peso { get; set; }
        public decimal PesoObjetivo { get; set; }
        public bool Completado { get; set; }
    }
}
