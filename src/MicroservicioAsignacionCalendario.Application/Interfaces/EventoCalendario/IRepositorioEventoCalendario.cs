using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MicroservicioAsignacionCalendario.Domain.Entities;

namespace MicroservicioAsignacionCalendario.Application.Interfaces.EventoCalendario
{
    public interface IRepositorioEventoCalendario
    {
        Task CancelarFuturosAsync(Guid idAlumno, DateTimeOffset desde, CancellationToken ct);
        Task AgregarRangoAsync(IEnumerable<Domain.Entities.EventoCalendario> eventos, CancellationToken ct);
    }
}
