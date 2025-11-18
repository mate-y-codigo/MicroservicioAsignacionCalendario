using MicroservicioAsignacionCalendario.Application.DTOs.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicioAsignacionCalendario.Application.Interfaces.Clients
{
    public interface IUsuariosClient
    {
        void SetAuthToken(string token);
        Task<UsuarioResponse> ObtenerUsuario(Guid id, CancellationToken ct = default);
    }
}
