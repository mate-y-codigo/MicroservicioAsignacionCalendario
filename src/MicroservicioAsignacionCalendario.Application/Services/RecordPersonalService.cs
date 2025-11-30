using Application.Interfaces.Command;
using Application.Interfaces.Query;
using AutoMapper;
using MicroservicioAsignacionCalendario.Application.CustomExceptions;
using MicroservicioAsignacionCalendario.Application.DTOs.RecordPersonal;
using MicroservicioAsignacionCalendario.Application.Interfaces.Clients;
using MicroservicioAsignacionCalendario.Application.Interfaces.RecordPersonal;
using MicroservicioAsignacionCalendario.Domain.Entities;

namespace MicroservicioAsignacionCalendario.Application.Services
{
    public class RecordPersonalService : IRecordPersonalService
    {
        private readonly IRecordPersonalQuery _query;
        private readonly IRecordPersonalCommand _command;
        private readonly ISesionRealizadaQuery _querySesionRealizada;
        private readonly IMapper _mapper;
        private readonly IPlanEntrenamientoClient _planEntrenamientoClient;
        private readonly IUsuariosClient _usuariosClient;
        public RecordPersonalService(IRecordPersonalQuery query, IRecordPersonalCommand command , IMapper mapper, ISesionRealizadaQuery sesionRealizadaQuery , IUsuariosClient usuariosClient, IPlanEntrenamientoClient planEntrenamientoClient)
        {
            _query = query;
            _command = command;
            _mapper = mapper;
            _querySesionRealizada = sesionRealizadaQuery;
            _usuariosClient = usuariosClient;
            _planEntrenamientoClient = planEntrenamientoClient;
        }

        public async Task ActualizarRecordPersonalAsync(EjercicioRegistro registro)
        {
            var sesion = await _querySesionRealizada.ObtenerSesionRealizadaPorId(registro.IdSesionRealizada);
            if (sesion == null)
                throw new NotFoundException($"La sesión realizada con Id {registro.IdSesionRealizada} no existe.");

            var idAlumnoPlan = sesion.IdAlumnoPlan;
            var idAlumno = sesion.AlumnoPlan.IdAlumno;
            var reps = registro.Repeticiones;
            var peso = registro.Peso;

            if (reps == 0 || peso == 0)
                return;

            decimal calculo1RM = Calcular1RM(peso, reps);

            var recordActual = await _query.ObtenerRecordPersonalPorId(idAlumno, registro.IdEjercicio);
            if (recordActual == null)
            {
                var nuevoRecord = _mapper.Map<RecordPersonal>(registro);
                nuevoRecord.IdAlumnoPlan = idAlumnoPlan;
                nuevoRecord.IdAlumno = idAlumno;
                nuevoRecord.Calculo1RM = calculo1RM;
                await _command.InsertarRecordPersonal(nuevoRecord);
            }
            else if (calculo1RM > recordActual.Calculo1RM)
            {
                recordActual.Calculo1RM = calculo1RM;
                recordActual.FechaRegistro = registro.FechaRealizacion;
                recordActual.PesoMax = peso;
                recordActual.Repeticiones = reps;
                recordActual.Series = registro.Series;
                recordActual.IdSesionRealizada = registro.IdSesionRealizada;
                recordActual.FechaRegistro = registro.FechaRealizacion;

                await _command.ActualizarRecordPersonal(recordActual);
            }
        }

        public async Task<List<RecordPersonalResponse>> ObtenerRecordsPersonalesAsync(RecordPersonalFilterRequest filtros)
        {
            if (filtros.IdEjercicio.HasValue)
            {
                var ejercicio = await _planEntrenamientoClient.ObtenerEjercicio(filtros.IdEjercicio.Value);
                if (ejercicio == null)
                    throw new NotFoundException($"El ejercicio con Id {filtros.IdEjercicio.Value} no existe.");
            }

            if (filtros.Desde.HasValue && filtros.Hasta.HasValue && filtros.Desde.Value > filtros.Hasta.Value)
                throw new BadRequestException("Rango de fechas inválido");

            var records = await _query.ObtenerRecordsPersonales(filtros);
            return _mapper.Map<List<RecordPersonalResponse>>(records);
        }

        private decimal Calcular1RM(decimal peso, int repeticiones)
        {
            if (repeticiones <= 0 || repeticiones >= 37)
                return peso;

            return peso / (1.0278m - (0.0278m * repeticiones));
        }
    }
}
