using MicroservicioAsignacionCalendario.Application.DTOs.PlanEntrenamiento;
using MicroservicioAsignacionCalendario.Application.DTOs.Usuario;
using MicroservicioAsignacionCalendario.Application.Interfaces.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicioAsignacionCalendario.Infrastructure.Clients
{
    public class UsuariosClient : IUsuariosClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _urlBase = "http://localhost:5099/api/Usuarios";
        public UsuariosClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UsuarioResponse> ObtenerUsuario(Guid id, CancellationToken ct)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<UsuarioResponse>(
                    $"{_urlBase}/{id}",
                    ct);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error al obtener un usuario: {ex.Message}");
                return null;
            }
        }
    }
}
