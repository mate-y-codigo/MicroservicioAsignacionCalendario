using MicroservicioAsignacionCalendario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Command
{
    public interface IAlumnoPlanCommand
    {
        Task InsertarAlumnoPlan(AlumnoPlan alumnoPlan);
    }
}
