using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace MicroservicioAsignacionCalendario.Application.Commands.AlumnoPlan
{
    public sealed record AsignarPlanCommand(
        Guid IdAlumno,
        Guid IdPlanEntrenamiento,
        Guid IdEntrenador,
        DateOnly FechaInicio,
        DateOnly? FechaFin,
        bool OverridePlanActivo,
        string? Notas
    ) : IRequest<AsignarPlanResultado>;
    
    public sealed class AsignarPlanResultado
    {
        [Required] public Guid IdAlumnoPlan { get; init; }
        [Required] public int EventosGenerados { get; init; }
        [Required] public bool PisoPlanAnterior { get; init; }
    }
}
