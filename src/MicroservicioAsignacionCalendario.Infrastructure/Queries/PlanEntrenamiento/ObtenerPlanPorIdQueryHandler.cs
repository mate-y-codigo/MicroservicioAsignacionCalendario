using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MicroservicioAsignacionCalendario.Application.DTOs.PlanEntrenamiento;
using MicroservicioAsignacionCalendario.Application.Interfaces.PlanEntrenamiento;

namespace MicroservicioAsignacionCalendario.Infrastructure.CQRS.Queries.PlanEntrenamiento
{
    public sealed class ObtenerPlanPorIdQueryHandler
        : IRequestHandler<ObtenerPlanPorIdQuery, PlanEntrenamientoResponse?>
    {
        private readonly IClientePlanes _clientePlanes;

        public ObtenerPlanPorIdQueryHandler(IClientePlanes clientePlanes)
        {
            _clientePlanes = clientePlanes;
        }

        public Task<PlanEntrenamientoResponse?> Handle(ObtenerPlanPorIdQuery request, CancellationToken cancellationToken)
        {
            return _clientePlanes.ObtenerPlanAsync(request.IdPlan, cancellationToken);
        }
    }
}
