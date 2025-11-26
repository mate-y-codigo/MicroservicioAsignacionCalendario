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
            // faltan arreglar un par de cosas, si se ponen todos los datos funciona


            var user = await _usuariosClient.ObtenerUsuario(filtros.IdAlumno.Value);
              
                if (user.RolId != 3)
                    throw new BadRequestException("El usuario ingresado no es alumno");

            var sesion = await _planEntrenamientoClient.ObtenerSesionEntrenamiento(filtros.IdSesionEntrenamiento.Value);

            var query = await _ejercicioRegistroQuery.ObtenerEjerciciosRegistros(filtros);

            var lista = new List<EjercicioRegistroResponse>();

            foreach (var elemento in query) {
                lista.Add(_mapper.Map<EjercicioRegistroResponse>(elemento));
            }

            return lista;

        }
    }
}
