using Application.Interfaces.Command;
using Application.Interfaces.EventoCalendario;
using Application.Interfaces.Query;
using AutoMapper;
using Interfaces.Query;
using MicroservicioAsignacionCalendario.Application.CustomExceptions;
using MicroservicioAsignacionCalendario.Application.DTOs.SesionRealizada;
using MicroservicioAsignacionCalendario.Application.Interfaces.AlumnoPlan;
using MicroservicioAsignacionCalendario.Application.Interfaces.Clients;
using MicroservicioAsignacionCalendario.Application.Interfaces.Query;
using MicroservicioAsignacionCalendario.Application.Interfaces.RecordPersonal;
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
        private readonly IAlumnoPlanService _alumnoPlanService;
        private readonly IRecordPersonalService _recordPersonalService;
        private readonly IMapper _mapper;
        private readonly IPlanEntrenamientoClient _planEntrenamientoClient;
        private readonly IUsuariosClient _usuariosClient;
        private readonly IEventoCalendarioService _eventoCalendarioService;


        public SesionRealizadaService(ISesionRealizadaQuery query, ISesionRealizadaCommand command, IAlumnoPlanQuery alumnoPlanQuery, IMapper mapper, IRecordPersonalService recordPersonalService, IPlanEntrenamientoClient plan, IUsuariosClient usuarios, IEventoCalendarioService eventoCalendarioService, IAlumnoPlanService alumnoPlanService)
        {
            _command = command;
            _query = query;
            _mapper = mapper;
            _alumnoPlanQuery = alumnoPlanQuery;
            _alumnoPlanService = alumnoPlanService;
            _recordPersonalService = recordPersonalService;
            _planEntrenamientoClient = plan;
            _usuariosClient = usuarios;
            _eventoCalendarioService = eventoCalendarioService;
        }

        public async Task<SesionRealizadaResponse> InsertarSesionRealizada(SesionRealizadaRequest req)
        {
            var alumno = await _usuariosClient.ObtenerUsuario(req.IdAlumno);
            if (alumno == null)
                throw new NotFoundException("El alumno no existe");

            var alumnoPlan = await _alumnoPlanQuery.ObtenerAlumnoPlan(req.IdAlumno);
            if (alumnoPlan == null)
                throw new NotFoundException("El alumno no tiene un plan activo");

            var sesionEntrenamientoPlanificada = await _planEntrenamientoClient.ObtenerSesionEntrenamiento(req.IdSesionEntrenamiento);
            if (sesionEntrenamientoPlanificada == null)
                throw new NotFoundException("La sesión de entrenamiento no existe");

            var sesionRealizada = _mapper.Map<SesionRealizada>(req);
            sesionRealizada.IdAlumnoPlan = alumnoPlan.Id;
            sesionRealizada.Estado = EstadoSesion.Completado;
            sesionRealizada.NombreSesion = sesionEntrenamientoPlanificada.Nombre;
            sesionRealizada.OrdenSesion = sesionEntrenamientoPlanificada.Orden;
            sesionRealizada.PesoCorporalAlumno = alumno.Peso;
            sesionRealizada.AlturaEnCmAlumno = alumno.Altura;

            sesionRealizada.EjerciciosRegistrados = new List<EjercicioRegistro>();
            foreach (var ej in req.RegistroEjercicios)
            {
                var sesionEjercicio = sesionEntrenamientoPlanificada.SesionesEjercicio
                    .FirstOrDefault(e => e.IdEjercicio == ej.IdEjercicio);

                if (sesionEjercicio == null)
                    throw new BadRequestException($"Ejercicio {ej.IdEjercicio} no válido o no pertenece a la sesión {req.IdSesionEntrenamiento}");

                var idSesionEjercicio = sesionEjercicio.Id;

                var sesionEjercicioDetalle = await _planEntrenamientoClient.ObtenerEjercicioSesion(idSesionEjercicio);
                if (sesionEjercicioDetalle == null)
                    throw new NotFoundException($"El ejercicio de sesión con Id {idSesionEjercicio} no existe.");


                Console.WriteLine("=== DEBUG EJERCICIO DETALLE ===");
                Console.WriteLine($"IdEjercicioSesion: {sesionEjercicioDetalle?.Id}");
                Console.WriteLine($"NombreEjercicio: {sesionEjercicioDetalle?.Ejercicio?.Nombre}");
                Console.WriteLine($"Musculo: {sesionEjercicioDetalle?.Ejercicio?.Musculo?.Nombre}");
                Console.WriteLine($"GrupoMuscular: {sesionEjercicioDetalle?.Ejercicio?.Musculo?.GrupoMuscular?.Nombre}");
                Console.WriteLine($"Categoria: {sesionEjercicioDetalle?.Ejercicio?.Categoria?.Nombre}");
                Console.WriteLine("===============================");
                var nuevoEjercicioRegistro = _mapper.Map<EjercicioRegistro>(ej);

                Console.WriteLine("=== DEBUG REGISTRO DESPUÉS DE MAPEAR ===");
                Console.WriteLine($"NombreEjercicio: {nuevoEjercicioRegistro.NombreEjercicio}");
                Console.WriteLine($"NombreMusculo: {nuevoEjercicioRegistro.NombreMusculo}");
                Console.WriteLine($"NombreGrupoMuscular: {nuevoEjercicioRegistro.NombreGrupoMuscular}");
                Console.WriteLine($"NombreCategoria: {nuevoEjercicioRegistro.NombreCategoria}");
                Console.WriteLine("=========================================");

                // Snapshots desde EjercicioSesion y SesionEntrenamiento
                nuevoEjercicioRegistro.IdEjercicioSesion = sesionEjercicioDetalle.Id;
                nuevoEjercicioRegistro.PesoObjetivo = (decimal)sesionEjercicioDetalle.PesoObjetivo;
                nuevoEjercicioRegistro.RepeticionesObjetivo = sesionEjercicioDetalle.RepeticionesObjetivo;
                nuevoEjercicioRegistro.SeriesObjetivo = sesionEjercicioDetalle.SeriesObjetivo;
                nuevoEjercicioRegistro.DescansoObjetivo = sesionEjercicioDetalle.Descanso;
                nuevoEjercicioRegistro.FechaRealizacion = sesionRealizada.FechaRealizacion ?? DateTime.UtcNow;

                // Snapshots desde Ejercicio
                nuevoEjercicioRegistro.NombreMusculo = nuevoEjercicioRegistro.NombreMusculo ?? "Sin datos";
                nuevoEjercicioRegistro.NombreGrupoMuscular = nuevoEjercicioRegistro.NombreGrupoMuscular ?? "Sin datos";
                nuevoEjercicioRegistro.NombreCategoria = nuevoEjercicioRegistro.NombreCategoria ?? "Sin datos";
                nuevoEjercicioRegistro.NombreEjercicio = nuevoEjercicioRegistro.NombreEjercicio ?? "Ejercicio";
                nuevoEjercicioRegistro.UrlDemoEjercicio = nuevoEjercicioRegistro.UrlDemoEjercicio ?? "";


                sesionRealizada.EjerciciosRegistrados.Add(nuevoEjercicioRegistro);
            }

            // Insertar Sesión Realizada
            await _command.InsertarSesionRealizadaCompleta(sesionRealizada);

            // Actualizar Records Personales
            foreach (var registro in sesionRealizada.EjerciciosRegistrados)
                await _recordPersonalService.ActualizarRecordPersonalAsync(registro);

            // Actualizar Progreso del Plan
            await _alumnoPlanService.ActualizarProgresoPlan(alumnoPlan.Id);

            // prox evento calendario
            await _eventoCalendarioService.CrearSiguienteEventoAsync(
                alumnoPlan,
                sesionRealizada.FechaRealizacion ?? DateTime.UtcNow,
                sesionEntrenamientoPlanificada.Nombre
            );

            return _mapper.Map<SesionRealizadaResponse>(sesionRealizada);
        }

        public async Task<List<SesionRealizadaListResponse>> ObtenerSesionesRealizadas(SesionRealizadaFilterRequest filtros)
        {
            //var alumno = await _usuariosClient.ObtenerUsuario(filtros.IdAlumno);
            //if (alumno == null)
            //    throw new NotFoundException($"El alumno con Id {filtros.IdAlumno} no existe.");

            var planEntrenamiento = await _planEntrenamientoClient.ObtenerPlanEntrenamiento(filtros.IdPlanEntrenamiento);
            if (planEntrenamiento == null)
                throw new NotFoundException($"El plan de entrenamiento con Id {filtros.IdPlanEntrenamiento} no existe.");

            if (filtros.Desde.HasValue && filtros.Hasta.HasValue && filtros.Desde.Value > filtros.Hasta.Value)
                throw new BadRequestException("Rango de fechas inválido");

            var sesiones = await _query.ObtenerSesionesRealizadas(filtros);
            if (sesiones == null || !sesiones.Any())
                return new List<SesionRealizadaListResponse>();

            var result = _mapper.Map<List<SesionRealizadaListResponse>>(sesiones);
            var nombrePlan = planEntrenamiento.Nombre;
            var sesionesDict = planEntrenamiento.SesionesEntrenamiento.ToDictionary(s => s.Id);

            foreach (var sesionDto in result)
            {
                sesionDto.NombrePlan = nombrePlan;

                if (sesionesDict.TryGetValue(sesionDto.IdSesionEntrenamiento, out var sesionInfo))
                {
                    sesionDto.NombreSesion = sesionInfo.Nombre;
                }
            }

            return result;
        }
    }
}
