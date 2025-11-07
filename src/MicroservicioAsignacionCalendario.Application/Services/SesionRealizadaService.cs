using Application.Interfaces.Command;
using Application.Interfaces.Query;
using AutoMapper;
using MicroservicioAsignacionCalendario.Application.CustomExceptions;
using MicroservicioAsignacionCalendario.Application.DTOs.SesionRealizada;
using MicroservicioAsignacionCalendario.Application.Interfaces.Clients;
using MicroservicioAsignacionCalendario.Application.Interfaces.Query;
using MicroservicioAsignacionCalendario.Application.Interfaces.SesionRealizada;
using MicroservicioAsignacionCalendario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicioAsignacionCalendario.Application.Services
{
    public class SesionRealizadaService : ISesionRealizadaService
    {
        private readonly ISesionRealizadaCommand _command;
        private readonly ISesionRealizadaQuery _query;
        private readonly IAlumnoPlanQuery _alumnoPlanQuery;
        private readonly IMapper _mapper;
        private readonly IPlanEntrenamientoClient _planEntrenamientoClient;
        private readonly IUsuariosClient _usuariosClient;
        public SesionRealizadaService(ISesionRealizadaQuery query, ISesionRealizadaCommand command, IAlumnoPlanQuery alumnoPlanQuery, IMapper mapper, IPlanEntrenamientoClient plan, IUsuariosClient usuarios)
        {
            _command = command;
            _query = query;
            _mapper = mapper;
            _alumnoPlanQuery = alumnoPlanQuery;
            _planEntrenamientoClient = plan;
            _usuariosClient = usuarios;
        }

        public async Task<SesionRealizadaResponse> InsertarSesionRealizada(SesionRealizadaRequest req)
        {
            // TO DO: IdAlumno debe venir desde authentication
            var alumno = await _usuariosClient.ObtenerUsuario(req.IdAlumno);
            if (alumno == null)
                throw new NotFoundException("El alumno no existe");

            var alumnoPlan = await _alumnoPlanQuery.ObtenerAlumnoPlan(alumno.Id);
            if (alumnoPlan == null)
                throw new NotFoundException("El alumno no tiene un plan activo");

            var sesionPlanificada = await _planEntrenamientoClient.ObtenerSesionEntrenamiento(req.IdSesionEntrenamiento);
            if(sesionPlanificada == null)
                throw new NotFoundException("La sesión de entrenamiento no existe");

            var sesionRealizada = _mapper.Map<SesionRealizada>(req);
            sesionRealizada.IdPlanAlumno = alumnoPlan.Id;
            sesionRealizada.Estado = EstadoSesion.Completado;

            var ejerciciosARegistrar = new List<EjercicioRegistro>();
            foreach(var ej in req.RegistroEjercicios)
            {
                var ejercicioPlanificado = sesionPlanificada.SesionesEjercicio
                    .FirstOrDefault(e => e.Id == ej.IdEjercicio);
                if (ejercicioPlanificado == null)
                    throw new BadRequestException($"Ejercicio {ej.IdEjercicio} no válido");

                var nuevoEjercicioRegistro = _mapper.Map<EjercicioRegistro>(ej);
                nuevoEjercicioRegistro.PesoObjetivo = (decimal)ejercicioPlanificado.PesoObjetivo;
                nuevoEjercicioRegistro.RepeticionesObjetivo = ejercicioPlanificado.RepeticionesObjetivo;
                nuevoEjercicioRegistro.SeriesObjetivo = ejercicioPlanificado.SeriesObjetivo;
                ejerciciosARegistrar.Add(nuevoEjercicioRegistro);
            }

            await _command.InsertarSesionRealizadaCompleta(sesionRealizada, ejerciciosARegistrar);
            sesionRealizada.EjerciciosRegistrados = ejerciciosARegistrar;
            return _mapper.Map<SesionRealizadaResponse>(sesionRealizada);
        }
    }
}
