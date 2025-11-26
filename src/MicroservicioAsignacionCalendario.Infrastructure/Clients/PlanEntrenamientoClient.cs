using MicroservicioAsignacionCalendario.Application.DTOs.EjercicioSesion;
using MicroservicioAsignacionCalendario.Application.DTOs.PlanEntrenamiento;
using MicroservicioAsignacionCalendario.Application.DTOs.SesionEntrenamiento;
using MicroservicioAsignacionCalendario.Application.DTOs.Usuario;
using MicroservicioAsignacionCalendario.Application.Interfaces.Clients;
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
       
        public PlanEntrenamientoClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PlanEntrenamientoResponse> ObtenerPlanEntrenamiento(Guid id, CancellationToken ct)
        {
               var response =  await _httpClient.GetAsync($"api/TrainingPlan/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    var errorBody = await response.Content.ReadAsStringAsync(ct);
                    Console.WriteLine($"Error al llamar a ConfigRutina API: {response.StatusCode} - {errorBody}");
                    return null;

                }
                return await response.Content.ReadFromJsonAsync<PlanEntrenamientoResponse>(cancellationToken: ct);
        }
  

        public async Task<SesionEntrenamientoResponse> ObtenerSesionEntrenamiento(Guid id, CancellationToken ct)
        {
            var response = await _httpClient.GetAsync($"api/TrainingSession/{id}", ct);
            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync(ct);
                Console.WriteLine($"Error al llamar a ConfigRutina API: {response.StatusCode} - {errorBody}");
                return null;

            }
            return await response.Content.ReadFromJsonAsync<SesionEntrenamientoResponse>(cancellationToken: ct);
        }

        public async Task<EjercicioSesionResponse> ObtenerEjercicioSesion(Guid idEjercicioSesion, CancellationToken ct)
        {
            var response = await _httpClient.GetAsync($"api/ExerciseSession/{idEjercicioSesion}", ct);
            if (!response.IsSuccessStatusCode) {
                var errorBody = await response.Content.ReadAsStringAsync(ct);
                Console.WriteLine($"Error al llamar a ConfigRutina API:{response.StatusCode} - {errorBody}");
                return null;
            }
            return await response.Content.ReadFromJsonAsync<EjercicioSesionResponse>(cancellationToken: ct);
             
        }

        public async Task<EjercicioResponse> ObtenerEjercicio(Guid idEjercicio, CancellationToken ct)
        {
            var response = await _httpClient.GetAsync($"api/Exercise/{idEjercicio}", ct);
            if (!response.IsSuccessStatusCode) { 
                var errorBody = await response.Content.ReadAsStringAsync (ct);
                Console.WriteLine($"Error al llamar a ConfigRutina API:{response.StatusCode} - {errorBody}");
                return null;
            }
            return await response.Content.ReadFromJsonAsync<EjercicioResponse>(cancellationToken: ct);
        }


    }
}
