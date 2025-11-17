using MicroservicioAsignacionCalendario.Application.DTOs.PlanEntrenamiento;
using MicroservicioAsignacionCalendario.Application.DTOs.Usuario;
using MicroservicioAsignacionCalendario.Application.Interfaces.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicioAsignacionCalendario.Infrastructure.Clients
{
    public class UsuariosClient : IUsuariosClient
    {
        private readonly HttpClient _httpClient;
        public UsuariosClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UsuarioResponse> ObtenerUsuario(Guid id, CancellationToken ct)
        {
            var response = await _httpClient.GetAsync($"api/Usuarios/{id}", ct);
            Console.WriteLine(response);
            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            //response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<UsuarioResponse>(cancellationToken: ct);
        }
    }
}
