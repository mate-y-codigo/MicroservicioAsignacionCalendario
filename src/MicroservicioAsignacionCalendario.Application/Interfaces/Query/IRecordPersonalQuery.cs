using MicroservicioAsignacionCalendario.Application.DTOs.RecordPersonal;
using MicroservicioAsignacionCalendario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Query
{
    public interface IRecordPersonalQuery
    {
        Task<List<RecordPersonal>> ObtenerRecordsPersonales(RecordPersonalFilterRequest filtros);
        Task<RecordPersonal?> ObtenerRecordPersonalPorId(Guid idAlumnoPlan, Guid IdEjercicio);
    }
}
