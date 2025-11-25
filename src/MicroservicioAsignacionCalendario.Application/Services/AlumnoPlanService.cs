using Application.DTOs.AlumnoPlan;
using Application.Interfaces.Command;
using Application.Interfaces.EventoCalendario;
using Application.Interfaces.Query;
using AutoMapper;
using Interfaces.Query;
using MicroservicioAsignacionCalendario.Application.CustomExceptions;
using MicroservicioAsignacionCalendario.Application.DTOs.AlumnoPlan;
using MicroservicioAsignacionCalendario.Application.DTOs.PlanEntrenamiento;
using MicroservicioAsignacionCalendario.Application.DTOs.Usuario;
using MicroservicioAsignacionCalendario.Application.Interfaces.AlumnoPlan;
using MicroservicioAsignacionCalendario.Application.Interfaces.Clients;
using MicroservicioAsignacionCalendario.Application.Interfaces.Query;
using MicroservicioAsignacionCalendario.Domain.Entities;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace Application.Services
{
    public class AlumnoPlanService : IAlumnoPlanService
    {
        private readonly IMapper _mapper;
        private readonly IPlanEntrenamientoClient _planEntrenamientoClient;
        private readonly IUsuariosClient _usuariosClient;
        private readonly IAlumnoPlanCommand _command;
        private readonly IAlumnoPlanQuery _query;
        private readonly ISesionRealizadaQuery _sesionRealizadaQuery;
        private readonly ISesionRealizadaCommand _sesionRealizadaCommand;
        private readonly IEventoCalendarioService _eventoCalendarioService;


        public AlumnoPlanService(IMapper mapper, IAlumnoPlanCommand command, IAlumnoPlanQuery query, ISesionRealizadaQuery sesionRealizadaQuery, ISesionRealizadaCommand sesionRealizadaCommand, IUsuariosClient usuariosClient, IPlanEntrenamientoClient planEntrenamientoClient, IEventoCalendarioService eventoCalendarioService)
        {
            _mapper = mapper;
            _planEntrenamientoClient = planEntrenamientoClient;
            _usuariosClient = usuariosClient;
            _command = command;
            _query = query;
            _sesionRealizadaQuery = sesionRealizadaQuery;
            _sesionRealizadaCommand = sesionRealizadaCommand;
            _eventoCalendarioService = eventoCalendarioService;
        }

        public async Task<AlumnoPlanResponse> AsignarPlanAsync(string token, AlumnoPlanRequest req)
        {
            _usuariosClient.SetAuthToken(token);
            var alumno = await _usuariosClient.ObtenerUsuario(req.IdAlumno);
            if (alumno == null)
                throw new BadRequestException("El alumno no existe.");

            if (alumno.Rol != "Alumno")
                throw new BadRequestException("El IdAlumno no corresponde a un Alumno.");

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
            alumnoPlan.TotalSesiones = plan.SesionesEntrenamiento.Count;
            alumnoPlan.TotalEjercicios = plan.SesionesEntrenamiento
                .Sum(s => s.SesionesEjercicio?.Count ?? 0);
            alumnoPlan.NombreAlumno = alumno.Nombre + " " + alumno.Apellido;
            alumnoPlan.IdSesionARealizar = primerSesionEntrenamiento.Id;
            alumnoPlan.Estado = EstadoAlumnoPlan.Activo;

            await _command.InsertarAlumnoPlan(alumnoPlan);
            // TO DO: Crear un evento Calendario..
            /* await _eventoCalendarioService.CrearEventosDePlanAsync(alumnoPlan, plan)*/

            return _mapper.Map<AlumnoPlanResponse>(alumnoPlan);
        }

        // TO DO: Implementar método
        public async Task<List<AlumnoPlanResponse>> ObtenerPlanesPorAlumnoAsync(Guid alumnoId)
        {

            var user = await _usuariosClient.ObtenerUsuario(alumnoId);

            if (user == null)
                throw new NotFoundException("El usuario ingresado no existe");

            if (user.RolId != 3)
                throw new BadRequestException("El usuario ingresado no es un alumno");

            var query = await _query.ObtenerPlanesPorAlumno(alumnoId);

            var lista = new List<AlumnoPlanResponse>();

            foreach (var elemento in query)
            {
                lista.Add(_mapper.Map<AlumnoPlanResponse>(elemento));
            }

            return lista;

        }

        public async Task<List<AlumnoPlanResponse>> ObtenerPlanesConFiltros(AlumnoPlanFilterRequest filtros)
        {
            if (filtros.IdAlumno != null && filtros.IdAlumno != Guid.Empty)
            {
                var idAlumnoValido = await _usuariosClient.ObtenerUsuario(filtros.IdAlumno!.Value);
                if (idAlumnoValido == null)
                    throw new NotFoundException("Id del alumno inválido");
            }

            if (filtros.IdPlanEntrenamiento != null && filtros.IdPlanEntrenamiento != Guid.Empty)
            {
                var idPlanEntrenamientoValido = _planEntrenamientoClient.ObtenerPlanEntrenamiento(filtros.IdPlanEntrenamiento!.Value);
                if (idPlanEntrenamientoValido == null)
                    throw new NotFoundException("Id del plan de entrenamiento inválido");
            }

            if (filtros.Estado != null)
            {
                if (!Enum.IsDefined(typeof(EstadoAlumnoPlan), filtros.Estado))
                    throw new BadRequestException("Estado inválido");
            }

            if (filtros.Desde != null && filtros.Hasta != null)
            {
                if (filtros.Desde > filtros.Hasta)
                    throw new BadRequestException("Rango de fechas inválido");
            }

            var planes = await _query.ObtenerPlanesConFiltros(filtros);
            var responses = new List<AlumnoPlanResponse>();
            foreach (var plan in planes)
            {
                var response = _mapper.Map<AlumnoPlanResponse>(plan);
                CalcularMetricas(response, plan.SesionesRealizadas);
                responses.Add(response);
            }
            return responses;
        }

        // este es el que usan para validar (micro config)
        public async Task<bool> PlanEntrenamientoAsignado(Guid idPlanEntrenamiento)
        {
            var planExists = await _planEntrenamientoClient.ObtenerPlanEntrenamiento(idPlanEntrenamiento);
            if (planExists == null)
                throw new NotFoundException("El plan de entrenamiento no existe.");

            return await _query.PlanEntrenamientoAsignado(idPlanEntrenamiento);
        }

        private void CalcularMetricas(AlumnoPlanResponse response, ICollection<SesionRealizada> sesionesRealizadas)
        {
            response.TotalSesiones = response.TotalSesiones;
            response.SesionesCompletadas = sesionesRealizadas.Count(sr => sr.Estado == EstadoSesion.Completado);
            response.ProgresoPorcentaje = response.TotalSesiones == 0 ? 0 :
                Math.Round((decimal)response.SesionesCompletadas / response.TotalSesiones * 100, 2);

            var ejerciciosDeSesionesCompletadas = sesionesRealizadas
                .Where(sr => sr.Estado == EstadoSesion.Completado)
                .SelectMany(sr => sr.EjerciciosRegistrados)
                .ToList();

            response.TotalEjercicios = response.TotalEjercicios;
            response.EjerciciosCumplidos = ejerciciosDeSesionesCompletadas.Count(er => er.Completado);
            response.AdherenciaPorcentaje = response.TotalEjercicios == 0 ? 0 :
                Math.Round((decimal)response.EjerciciosCumplidos / response.TotalEjercicios * 100, 2);
        }

        public async Task ActualizarProgresoPlan(Guid idAlumnoPlan)
        {
            var alumnoPlan = await _query.ObtenerAlumnoPlanPorId(idAlumnoPlan);
            if (alumnoPlan == null)
                throw new NotFoundException("El plan del alumno no existe.");

            var sesionesCompletadas = await _sesionRealizadaQuery.ObtenerSesionesCompletadas(idAlumnoPlan);

            var planEntrenamiento = await _planEntrenamientoClient.ObtenerPlanEntrenamiento(alumnoPlan.IdPlanEntrenamiento);
            var sesionesOrdenadas = planEntrenamiento.SesionesEntrenamiento.OrderBy(s => s.Orden).ToList();

            var ultimoOrdenCompletado = sesionesCompletadas
                .OrderByDescending(sr => sr.OrdenSesion)
                .Select(sr => sr.OrdenSesion)
                .FirstOrDefault();

            var siguienteOrden = ultimoOrdenCompletado + 1;
            var siguienteSesion = sesionesOrdenadas.FirstOrDefault(s => s.Orden == siguienteOrden);

            var estadoAnterior = alumnoPlan.Estado;
            var idSesionAnterior = alumnoPlan.IdSesionARealizar;

            alumnoPlan.IdSesionARealizar = siguienteSesion?.Id ?? Guid.Empty;

            if (sesionesCompletadas.Count >= alumnoPlan.TotalSesiones)
            {
                alumnoPlan.Estado = EstadoAlumnoPlan.Finalizado;
            }

            // 4. Solo guardar si hubo cambios
            if (estadoAnterior != alumnoPlan.Estado || idSesionAnterior != alumnoPlan.IdSesionARealizar)
                await _command.ActualizarAlumnoPlan(alumnoPlan);
        }
    }
}
