
using MicroservicioAsignacionCalendario.Application.DTOs.EventoCalendario;
using MicroservicioAsignacionCalendario.Application.DTOs.PlanEntrenamiento;
using MicroservicioAsignacionCalendario.Domain.Entities;

namespace Application.Interfaces.EventoCalendario
{
    public interface IEventoCalendarioService
    {
        Task CrearEventosDePlanAsync(MicroservicioAsignacionCalendario.Domain.Entities.AlumnoPlan alumnoPlan, PlanEntrenamientoResponse plan);

        Task CrearEventosDePlanAsync(AlumnoPlan alumnoPlan, PlanEntrenamientoResponse plan);
    }
}