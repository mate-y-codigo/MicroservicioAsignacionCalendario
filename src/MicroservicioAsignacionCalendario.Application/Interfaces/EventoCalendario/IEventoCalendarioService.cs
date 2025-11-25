
using MicroservicioAsignacionCalendario.Application.DTOs.EventoCalendario;
using MicroservicioAsignacionCalendario.Application.DTOs.PlanEntrenamiento;
using MicroservicioAsignacionCalendario.Domain.Entities;

namespace Application.Interfaces.EventoCalendario
{
    public interface IEventoCalendarioService
    {
        Task<List<EventoCalendarioResponse>> ObtenerEventosAsync(EventoCalendarioFilterRequest filtros);
        Task CrearEventosDePlanAsync(AlumnoPlan alumnoPlan, PlanEntrenamientoResponse plan);
        Task CrearPrimerEventoAsync(AlumnoPlan alumnoPlan);

    }
}