using MicroservicioAsignacionCalendario.Application.DTOs.PlanEntrenamiento;
using MicroservicioAsignacionCalendario.Application.Interfaces.Micro_PlanEntrenamiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MicroservicioAsignacionCalendario.Infrastructure.Clients
{
    public class PlanEntrenamientoClient : IPlanEntrenamientoClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _urlBase = "http://localhost:5097/api/TrainingPlan";

        public PlanEntrenamientoClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PlanEntrenamientoResponse> GetPlanEntrenamiento(Guid id, CancellationToken ct)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<PlanEntrenamientoResponse>(
                    $"{_urlBase}/{id}",
                    ct);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error fetching training plan: {ex.Message}");
                return null;
            }
        }
    }
}
