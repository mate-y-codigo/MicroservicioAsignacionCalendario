using MicroservicioAsignacionCalendario.Application.DTOs.SesionRealizada;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicioAsignacionCalendario.Application.Interfaces.SesionRealizada
{
    public interface ISesionRealizadaService
    {
        Task<SesionRealizadaResponse> InsertarSesionRealizada(SesionRealizadaRequest req);
        Task<List<SesionRealizadaListResponse>> ObtenerSesionesRealizadas(SesionRealizadaFilterRequest req);
    }
}
