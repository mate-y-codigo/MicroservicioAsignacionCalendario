using MicroservicioAsignacionCalendario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Interfaces.Command
{
    public interface IEventoCalendarioCommand
    {

        //sacar despues
        Task InsertarEventosCalendario(IEnumerable<MicroservicioAsignacionCalendario.Domain.Entities.EventoCalendario> eventos);

        Task InsertarEventoCalendario(MicroservicioAsignacionCalendario.Domain.Entities.EventoCalendario evento);

    }
}
