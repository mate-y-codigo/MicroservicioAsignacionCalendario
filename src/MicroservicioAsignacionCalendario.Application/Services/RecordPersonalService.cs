using Application.Interfaces.Query;
using AutoMapper;
using MicroservicioAsignacionCalendario.Application.CustomExceptions;
using MicroservicioAsignacionCalendario.Application.DTOs.RecordPersonal;
using MicroservicioAsignacionCalendario.Application.Interfaces.Clients;
using MicroservicioAsignacionCalendario.Application.Interfaces.RecordPersonal;

namespace MicroservicioAsignacionCalendario.Application.Services
{
    public class RecordPersonalService : IRecordPersonalService
    {
        private readonly IRecordPersonalQuery _query;
        private readonly IMapper _mapper;
        private readonly IPlanEntrenamientoClient _planEntrenamientoClient;
        private readonly IUsuariosClient _usuariosClient;
        public RecordPersonalService(IRecordPersonalQuery query, IMapper mapper, IUsuariosClient usuariosClient, IPlanEntrenamientoClient planEntrenamientoClient)
        {
            _query = query;
            _mapper = mapper;
            _usuariosClient = usuariosClient;
            _planEntrenamientoClient = planEntrenamientoClient;
        }
        public async Task<List<RecordPersonalResponse>> ObtenerRecordsPersonalesAsync(RecordPersonalFilterRequest filtros)
        {
            //var usuario = await _usuariosClient.ObtenerUsuario(filtros.IdAlumno);
            //if (usuario == null)
            //    throw new NotFoundException($"El usuario con Id {filtros.IdAlumno} no existe.");

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
    }
}
