using Application.Interfaces.Command;
using Application.Interfaces.Query;
using AutoMapper;
using MicroservicioAsignacionCalendario.Application.CustomExceptions;
using MicroservicioAsignacionCalendario.Application.DTOs.EjercicioRegistro;
using MicroservicioAsignacionCalendario.Application.Interfaces.Clients;
using MicroservicioAsignacionCalendario.Application.Interfaces.RegistroEjercicio;
using MicroservicioAsignacionCalendario.Domain.Entities;

namespace MicroservicioAsignacionCalendario.Application.Services
{
    public class EjercicioRegistroService : IEjercicioRegistroService
    {
        private readonly IMapper _mapper;
        private readonly IPlanEntrenamientoClient _planEntrenamientoClient;
        private readonly ISesionRealizadaQuery _sesionRealizadaQuery;
        private readonly IEjercicioRegistroCommand _command;
        public EjercicioRegistroService(IMapper mapper, IEjercicioRegistroCommand command, IPlanEntrenamientoClient planEntrenamientoClient, ISesionRealizadaQuery query)
        {
            _command = command;
            _mapper = mapper;
            _planEntrenamientoClient = planEntrenamientoClient;
            _sesionRealizadaQuery = query;
        }

        public async Task<EjercicioRegistroResponse> RegistrarEjercicioAsync(EjercicioRegistroRequest req)
        {
            var sesionRealizada = await _sesionRealizadaQuery.ObtenerSesionRealizadaPorId(req.IdSesionRealizada);
            if (sesionRealizada == null)
                throw new NotFoundException("La sesión realizada no existe");

            var sesionPlanificada = await _planEntrenamientoClient.ObtenerSesionEntrenamiento(sesionRealizada.IdSesionEntrenamiento);
            if (sesionPlanificada == null)
                throw new NotFoundException("La sesion de entrenamiento no existe");

            var ejercicioPlanificado = sesionPlanificada.SesionesEjercicio.FirstOrDefault(ej => ej.Id == req.IdEjercicio);
            if (ejercicioPlanificado == null)
                throw new NotFoundException("El ejercicio no existe o no pertenece a la sesion de entrenamiento");

            var ejercicioRegistro = _mapper.Map<EjercicioRegistro>(req);
            ejercicioRegistro.PesoObjetivo = (decimal)ejercicioPlanificado.PesoObjetivo;
            ejercicioRegistro.SeriesObjetivo = ejercicioPlanificado.SeriesObjetivo;
            ejercicioRegistro.RepeticionesObjetivo = ejercicioPlanificado.RepeticionesObjetivo;

            await _command.InsertarEjercicioRegistro(ejercicioRegistro);

            return _mapper.Map<EjercicioRegistroResponse>(ejercicioRegistro);
        }

        // TO DO: Implementar método
        public Task<List<EjercicioRegistroResponse>> ObtenerRegistrosAsync(EjercicioRegistroFilterRequest filtros)
        {
            throw new NotImplementedException();
        }
    }
}
