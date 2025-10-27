using MicroservicioAsignacionCalendario.Application.CustomExceptions;
using MicroservicioAsignacionCalendario.Application.DTOs.AlumnoPlan;
using MicroservicioAsignacionCalendario.Application.DTOs.PlanEntrenamiento;
using MicroservicioAsignacionCalendario.Application.Interfaces.AlumnoPlan;
using MicroservicioAsignacionCalendario.Application.Interfaces.Command;
using MicroservicioAsignacionCalendario.Application.Interfaces.Micro_PlanEntrenamiento;
using MicroservicioAsignacionCalendario.Application.Interfaces.Query;
using MicroservicioAsignacionCalendario.Domain.Entities;

namespace MicroservicioAsignacionCalendario.Application.Services
{
    public class AlumnoPlanService : IAlumnoPlanService
    {
        private readonly IPlanEntrenamientoClient _planEntrenamientoClient;
        private readonly IAlumnoPlanCommand _command;
        private readonly IAlumnoPlanQuery _query;
        public AlumnoPlanService(IAlumnoPlanCommand command, IAlumnoPlanQuery query, IPlanEntrenamientoClient planEntrenamientoClient)
        {
            _planEntrenamientoClient = planEntrenamientoClient;
            _command = command;
            _query = query;
        }

        public async Task<AlumnoPlanResponse> AsignarPlanAsync(AlumnoPlanRequest req)
        {
            // TO DO: Verificar si el alumno existe

            PlanEntrenamientoResponse plan = await _planEntrenamientoClient.GetPlanEntrenamiento(req.IdPlanEntrenamiento);
            if (plan == null)
                throw new BadRequestException("El plan de entrenamiento no existe.");

            if (req.FechaInicio > req.FechaFin || req.FechaInicio < DateTime.Now)
                throw new BadRequestException("Rango de fechas inválido");

            var idSesionActual = plan.TrainingSessions
                .FirstOrDefault(s => s.Order == 1);

            var planAsociado = await _query.ObtenerAlumnoPlan(req.IdAlumno);
            if (planAsociado != null)
                throw new ConflictException("El alumno ya tiene un plan de entrenamiento asignado.");

            var alumnoPlan = new AlumnoPlan
            {
                IdAlumno = req.IdAlumno,
                IdPlanEntrenamiento = req.IdPlanEntrenamiento,
                FechaInicio = req.FechaInicio,
                FechaFin = req.FechaFin,
                IntervaloDiasDescanso = req.IntervaloDiasDescanso,
                Notas = req.Notas != null ? req.Notas.Trim() : "",
                Estado = Estado.Activo,
                IdSesionActual = idSesionActual != null ? idSesionActual.Id : Guid.Empty,
            };

            await _command.InsertarAlumnoPlan(alumnoPlan);

            var alumnoPlanResponse = new AlumnoPlanResponse
            {
                Id = alumnoPlan.Id,
                IdAlumno = alumnoPlan.IdAlumno,
                IdPlanEntrenamiento = alumnoPlan.IdPlanEntrenamiento,
                FechaInicio = alumnoPlan.FechaInicio,
                FechaFin = alumnoPlan.FechaFin,
                IntervaloDiasDescanso = alumnoPlan.IntervaloDiasDescanso,
                Notas = alumnoPlan.Notas,
                Estado = alumnoPlan.Estado,
                IdSesionActual = alumnoPlan.IdSesionActual
            };

            return alumnoPlanResponse;
        }

        // TO DO: Implementar método
        public async Task<List<AlumnoPlanResponse>> ObtenerPlanesPorAlumnoAsync(Guid alumnoId)
        {
            throw new NotImplementedException();
        }
    }
}
