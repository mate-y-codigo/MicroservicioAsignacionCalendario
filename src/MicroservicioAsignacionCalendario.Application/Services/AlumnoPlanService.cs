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
using AutoMapper;
using Application.Interfaces.EventoCalendario;
using System.Runtime.CompilerServices;
using Interfaces.Query;

namespace Application.Services
{
    public class AlumnoPlanService : IAlumnoPlanService
    {
        private readonly IMapper _mapper;
        private readonly IPlanEntrenamientoClient _planEntrenamientoClient;
        private readonly IUsuariosClient _usuariosClient;
        private readonly IAlumnoPlanCommand _command;
        private readonly IAlumnoPlanQuery _query;
        private readonly IEventoCalendarioService _eventoCalendarioService;


        public AlumnoPlanService(IMapper mapper, IAlumnoPlanCommand command, IAlumnoPlanQuery query, IUsuariosClient usuariosClient, IPlanEntrenamientoClient planEntrenamientoClient, IEventoCalendarioService eventoCalendarioService)
        {
            _mapper = mapper;
            _planEntrenamientoClient = planEntrenamientoClient;
            _usuariosClient = usuariosClient;
            _command = command;
            _query = query;
            _eventoCalendarioService = eventoCalendarioService;
        }

        public async Task<AlumnoPlanResponse> AsignarPlanAsync(string token, AlumnoPlanRequest req)
        {
            _usuariosClient.SetAuthToken(token);
            var usuario = await _usuariosClient.ObtenerUsuario(req.IdAlumno);
            if (usuario == null)
                throw new BadRequestException("El alumno no existe.");

            if (usuario.Rol != "Alumno")
                throw new BadRequestException("El ID proporcionado no corresponde a un Alumno.");

            var plan = await _planEntrenamientoClient.ObtenerPlanEntrenamiento(req.IdPlanEntrenamiento);
            if (plan == null)
                throw new BadRequestException("El plan de entrenamiento no existe.");

            if (req.FechaInicio > req.FechaFin || req.FechaInicio.Date < DateTime.Today)
                throw new BadRequestException("Rango de fechas inválido. La fecha de inicio no puede ser en el pasado.");

            var primerSesionEntrenamiento = plan.SesionesEntrenamiento
                .FirstOrDefault(s => s.Orden == 1);
            if (primerSesionEntrenamiento == null)
                throw new BadRequestException("El plan de entrenamiento no tiene sesiones definidas o está corrupto.");

            var planAsociado = await _query.ObtenerAlumnoPlan(req.IdAlumno);
            if (planAsociado != null)
                throw new ConflictException("El alumno ya tiene un plan de entrenamiento asignado.");

            var alumnoPlan = _mapper.Map<AlumnoPlan>(req);
            alumnoPlan.NombrePlan = plan.Nombre;
            alumnoPlan.DescripcionPlan = plan.Descripcion;
            alumnoPlan.IdSesionARealizar = primerSesionEntrenamiento.Id;
            alumnoPlan.Estado = EstadoAlumnoPlan.Activo;

            await _command.InsertarAlumnoPlan(alumnoPlan);

            // TO DO: Crear un evento Calendario..
           /* await _eventoCalendarioService.CrearEventosDePlanAsync(alumnoPlan, plan)*/;

            return _mapper.Map<AlumnoPlanResponse>(alumnoPlan);
        }

        // TO DO: Implementar método
        public async Task<List<AlumnoPlanResponse>> ObtenerPlanesPorAlumnoAsync(Guid alumnoId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> PlanEntrenamientoAsignado(Guid idPlanEntrenamiento)
        {
            var planExists = await _planEntrenamientoClient.ObtenerPlanEntrenamiento(idPlanEntrenamiento);
            if (planExists == null)
                throw new NotFoundException("El plan de entrenamiento no existe.");

            return await _query.PlanEntrenamientoAsignado(idPlanEntrenamiento);
        }
    }
}
