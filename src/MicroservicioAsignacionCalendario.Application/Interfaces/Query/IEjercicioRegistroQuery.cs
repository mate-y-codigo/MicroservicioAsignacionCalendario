using MicroservicioAsignacionCalendario.Application.DTOs.EjercicioRegistro;
using MicroservicioAsignacionCalendario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicioAsignacionCalendario.Application.Interfaces.Query
{
    public interface IEjercicioRegistroQuery
    {
        Task<List<EjercicioRegistro>> ObtenerEjerciciosRegistros(EjercicioRegistroFilterRequest filtros);
    }
}
