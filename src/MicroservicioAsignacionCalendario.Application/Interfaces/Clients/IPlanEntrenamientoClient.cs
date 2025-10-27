using MicroservicioAsignacionCalendario.Application.DTOs.PlanEntrenamiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicioAsignacionCalendario.Application.Interfaces.Clients
{
    public interface IPlanEntrenamientoClient
    {
        Task<PlanEntrenamientoResponse> ObtenerPlanEntrenamiento(Guid id, CancellationToken ct = default);
    }
}
