using MicroservicioAsignacionCalendario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Command
{
    public interface IRecordPersonalCommand
    {
        Task InsertarRecordPersonal(RecordPersonal record);
        Task ActualizarRecordPersonal(RecordPersonal record);
    }
}
