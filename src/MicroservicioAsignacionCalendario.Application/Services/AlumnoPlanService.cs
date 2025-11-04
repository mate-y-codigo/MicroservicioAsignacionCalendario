using MicroservicioAsignacionCalendario.Application.CustomExceptions;
using MicroservicioAsignacionCalendario.Application.DTOs.AlumnoPlan;
using MicroservicioAsignacionCalendario.Application.DTOs.PlanEntrenamiento;
using MicroservicioAsignacionCalendario.Application.DTOs.Usuario;
using MicroservicioAsignacionCalendario.Application.Interfaces.AlumnoPlan;
using MicroservicioAsignacionCalendario.Application.Interfaces.Clients;
using Application.Interfaces.Command;
using MicroservicioAsignacionCalendario.Application.Interfaces.Query;
using MicroservicioAsignacionCalendario.Domain.Entities;
using Application.DTOs.AlumnoPlan;

namespace Application.Services
{
    public class AlumnoPlanService : IAlumnoPlanService
    {
        private readonly IPlanEntrenamientoClient _planEntrenamientoClient;
        private readonly IUsuariosClient _usuariosClient;
        private readonly IAlumnoPlanCommand _command;
        private readonly IAlumnoPlanQuery _query;
        public AlumnoPlanService(IAlumnoPlanCommand command, IAlumnoPlanQuery query, IUsuariosClient usuariosClient, IPlanEntrenamientoClient planEntrenamientoClient)
        {
            _planEntrenamientoClient = planEntrenamientoClient;
            _usuariosClient = usuariosClient;
            _command = command;
            _query = query;
        }

        public async Task<AlumnoPlanResponse> AsignarPlanAsync(AlumnoPlanRequest req)
        {
            Task<UsuarioResponse> usuarioTask = _usuariosClient.ObtenerUsuario(req.IdAlumno);
            Task<PlanEntrenamientoResponse> planTask = _planEntrenamientoClient.ObtenerPlanEntrenamiento(req.IdPlanEntrenamiento);
            await Task.WhenAll(usuarioTask, planTask);

            UsuarioResponse usuario = await usuarioTask;
            PlanEntrenamientoResponse plan = await planTask;

            if (usuario == null)
                throw new BadRequestException("El alumno no existe.");

            // TO DO: Validar que el usuario sea un alumno (rol)

            if (plan == null)
                throw new BadRequestException("El plan de entrenamiento no existe.");

            if (req.FechaInicio > req.FechaFin || req.FechaInicio < DateTime.UtcNow)
                throw new BadRequestException("Rango de fechas inválido");

            var primerSesionEntrenamiento = plan.SesionesEntrenamiento
                .SingleOrDefault(s => s.Orden == 1);

            if (primerSesionEntrenamiento == null)
                throw new BadRequestException("El plan de entrenamiento no tiene sesiones definidas o está corrupto.");

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
                Estado = EstadoAlumnoPlan.Activo,
                IdSesionActual = primerSesionEntrenamiento.Id,
            };

            await _command.InsertarAlumnoPlan(alumnoPlan);

            return new AlumnoPlanResponse
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
        }

        // TO DO: Implementar método
        public async Task<List<AlumnoPlanResponse>> ObtenerPlanesPorAlumnoAsync(Guid alumnoId)
        {
            throw new NotImplementedException();
        }
    }
}
