using MicroservicioAsignacionCalendario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Query
{
    public interface IAlumnoPlanQuery
    {
        Task<AlumnoPlan> ObtenerAlumnoPlan(Guid id);
        Task<bool> PlanEntrenamientoAsignado(Guid idPlanEntrenamiento);
        Task<List<AlumnoPlan>> ObtenerPlanesPorAlumno(Guid IdAlumno);
    }
}
