using Application.Interfaces.Command;
using Application.Interfaces.Query;
using AutoMapper;
using MicroservicioAsignacionCalendario.Application.CustomExceptions;
using MicroservicioAsignacionCalendario.Application.DTOs.EjercicioRegistro;
using MicroservicioAsignacionCalendario.Application.Interfaces.Clients;
using MicroservicioAsignacionCalendario.Application.Interfaces.Query;
using MicroservicioAsignacionCalendario.Application.Interfaces.RegistroEjercicio;
using MicroservicioAsignacionCalendario.Domain.Entities;

namespace MicroservicioAsignacionCalendario.Application.Services
{
    public class EjercicioRegistroService : IEjercicioRegistroService
    {
        private readonly IMapper _mapper;
        private readonly IPlanEntrenamientoClient _planEntrenamientoClient;
        private readonly ISesionRealizadaQuery _sesionRealizadaQuery;
        private readonly IUsuariosClient _usuariosClient;
        private readonly IEjercicioRegistroQuery _ejercicioRegistroQuery;
        public EjercicioRegistroService(ISesionRealizadaQuery query, IMapper mapper, IPlanEntrenamientoClient planEntrenamientoClient, IUsuariosClient usuariosClient, IEjercicioRegistroQuery ejercicioRegistroQuery)
        {
            _mapper = mapper;
            _planEntrenamientoClient = planEntrenamientoClient;
            _sesionRealizadaQuery = query;
            _usuariosClient = usuariosClient;
            _ejercicioRegistroQuery = ejercicioRegistroQuery;
        }

        // TO DO: Implementar método
        public async Task<List<EjercicioRegistroResponse>> ObtenerRegistrosAsync(EjercicioRegistroFilterRequest filtros)
        {
            // funciona, se podria intentar validar los datos del front, pero si ya se pide como guid mismo asp lo hace saltar
            
            var query = await _ejercicioRegistroQuery.ObtenerEjerciciosRegistros(filtros);

            var lista = new List<EjercicioRegistroResponse>();

            foreach (var elemento in query) {
                lista.Add(_mapper.Map<EjercicioRegistroResponse>(elemento));
            }

            return lista;

        }
    }
}
