namespace MicroservicioAsignacionCalendario.Domain.Entities
{
    public enum EstadoAlumnoPlan
    {
        Activo = 1,
        Finalizado = 2,
    }

    public enum EstadoSesion
    {
        Pendiente = 1,
        Completado = 2
    }

    public enum EstadoEvento
    {
        Programado = 1,
        Completado = 2
    }
}
