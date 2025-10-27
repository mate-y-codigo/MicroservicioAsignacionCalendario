using MicroservicioAsignacionCalendario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicioAsignacionCalendario.Application.Interfaces.Query
{
    public interface IAlumnoPlanQuery
    {
        Task<MicroservicioAsignacionCalendario.Domain.Entities.AlumnoPlan> ObtenerAlumnoPlan(Guid id);
    }
}
