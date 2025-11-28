using System;
using System.Collections.Generic;
using DomainEntities = MicroservicioAsignacionCalendario.Domain.Entities;

namespace MicroservicioAsignacionCalendario.Application.Interfaces.Query
{
    // Alias DomainEntities para evitar cualquier choque de nombres
    public interface IMetricsQuery
    {
        Task<List<DomainEntities.AlumnoPlan>> GetPlanesDelEntrenadorAsync(Guid idEntrenador);

        Task<List<DomainEntities.SesionRealizada>> GetSesionesRealizadasAsync(
            Guid idEntrenador,
            DateTime desde,
            DateTime hasta);

        Task<List<DomainEntities.EjercicioRegistro>> GetEjerciciosRegistradosAsync(
            Guid idEntrenador,
            DateTime desde,
            DateTime hasta);

        Task<List<DomainEntities.EventoCalendario>> GetEventosProgramadosAsync(
            Guid idEntrenador,
            DateTime desde,
            DateTime hasta);

        Task<List<DomainEntities.RecordPersonal>> GetRecordsPersonalesAsync(
            Guid idEntrenador,
            DateTime desde,
            DateTime hasta);
    }
}
