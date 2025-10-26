using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MicroservicioAsignacionCalendario.Application.DTOs.PlanEntrenamiento;

namespace MicroservicioAsignacionCalendario.Application.Interfaces.PlanEntrenamiento
{
    public interface IClientePlanes
    {
        Task<PlanEntrenamientoResponse?> ObtenerPlanAsync(Guid idPlan, CancellationToken ct);
    }
}
