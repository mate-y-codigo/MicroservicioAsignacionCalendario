using MicroservicioAsignacionCalendario.Application.DTOs.PlanEntrenamiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicioAsignacionCalendario.Application.Interfaces.Micro_PlanEntrenamiento
{
    public interface IPlanEntrenamientoClient
    {
        Task<PlanEntrenamientoResponse> GetPlanEntrenamiento(Guid id, CancellationToken ct = default);
    }
}
