using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MicroservicioAsignacionCalendario.Application.DTOs.PlanEntrenamiento;

namespace MicroservicioAsignacionCalendario.Infrastructure.CQRS.Queries.PlanEntrenamiento
{
    public sealed record ObtenerPlanPorIdQuery(Guid IdPlan) : IRequest<PlanEntrenamientoResponse?>;
}
