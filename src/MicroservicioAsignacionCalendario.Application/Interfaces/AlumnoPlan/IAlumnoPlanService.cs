using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MicroservicioAsignacionCalendario.Application.DTOs.AlumnoPlan;

namespace MicroservicioAsignacionCalendario.Application.Interfaces.AlumnoPlan
{
    public interface IAlumnoPlanService
    {
        Task<AsignarPlanResponse> AsignarPlanAsync(AsignarPlanRequest request);
        Task<object> ObtenerPlanesPorAlumnoAsync(Guid idAlumno);
    }
}
