
using MicroservicioAsignacionCalendario.Application.DTOs.EventoCalendario;
using MicroservicioAsignacionCalendario.Application.DTOs.PlanEntrenamiento;
using MicroservicioAsignacionCalendario.Domain.Entities;

namespace Application.Interfaces.EventoCalendario
{
    public interface IEventoCalendarioService
    {

        // este queda deprecado
        Task<List<EventoCalendarioResponse>> ObtenerEventosAsync(EventoCalendarioFilterRequest filtros);
        Task CrearEventosDePlanAsync(AlumnoPlan alumnoPlan, PlanEntrenamientoResponse plan);
        Task CrearPrimerEventoAsync(AlumnoPlan alumnoPlan, string nombreSesion);
        Task CrearSiguienteEventoAsync(AlumnoPlan alumnoPlan, DateTime fechaBase, string nombreSesion);

    }
}