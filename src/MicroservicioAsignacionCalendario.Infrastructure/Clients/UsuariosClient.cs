using MicroservicioAsignacionCalendario.Application.DTOs.PlanEntrenamiento;
using MicroservicioAsignacionCalendario.Application.DTOs.Usuario;
using MicroservicioAsignacionCalendario.Application.Interfaces.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
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

        public void SetAuthToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<UsuarioResponse> ObtenerUsuario(Guid id, CancellationToken ct)
        {
            var response = await _httpClient.GetAsync($"api/Usuarios/{id}", ct);
            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync(ct);
                Console.WriteLine($"Error al llamar a Usuarios API: {response.StatusCode} - {errorBody}");
                return null;
            }
            
            return await response.Content.ReadFromJsonAsync<UsuarioResponse>(cancellationToken: ct);
        }
    }
}
