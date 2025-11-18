using MicroservicioAsignacionCalendario.Application.DTOs.EjercicioSesion;
using MicroservicioAsignacionCalendario.Application.DTOs.PlanEntrenamiento;
using MicroservicioAsignacionCalendario.Application.DTOs.SesionEntrenamiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicioAsignacionCalendario.Application.Interfaces.Clients
{
    public interface IPlanEntrenamientoClient
    {
        Task<PlanEntrenamientoResponse> ObtenerPlanEntrenamiento(Guid id, CancellationToken ct = default);
        Task<SesionEntrenamientoResponse> ObtenerSesionEntrenamiento(Guid id, CancellationToken ct = default);
        Task<EjercicioResponse> ObtenerEjercicio(Guid id, CancellationToken ct = default);
        //Task<EjercicioSesionResponse> ObtenerEjercicioSesion(Guid id, CancellationToken ct = default);
    }
}
