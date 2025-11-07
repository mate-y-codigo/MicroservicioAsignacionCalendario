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
        public EjercicioRegistroService(ISesionRealizadaQuery query, IMapper mapper, IPlanEntrenamientoClient planEntrenamientoClient)
        {
            _mapper = mapper;
            _planEntrenamientoClient = planEntrenamientoClient;
            _sesionRealizadaQuery = query;
        }

        // TO DO: Implementar método
        public Task<List<EjercicioRegistroResponse>> ObtenerRegistrosAsync(EjercicioRegistroFilterRequest filtros)
        {
            throw new NotImplementedException();
        }
    }
}
