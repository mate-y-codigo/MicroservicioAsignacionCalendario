using MicroservicioAsignacionCalendario.Application.DTOs.SesionRealizada;
using MicroservicioAsignacionCalendario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Query
{
    public interface ISesionRealizadaQuery
    {
        Task<SesionRealizada> ObtenerSesionRealizadaPorId(Guid id);
        Task<List<SesionRealizada>> ObtenerSesionesRealizadas(SesionRealizadaFilterRequest filtros);
    }
}
