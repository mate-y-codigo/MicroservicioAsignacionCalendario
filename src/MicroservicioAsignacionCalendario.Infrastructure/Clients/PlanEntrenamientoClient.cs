using MicroservicioAsignacionCalendario.Application.DTOs.EjercicioSesion;
using MicroservicioAsignacionCalendario.Application.DTOs.PlanEntrenamiento;
using MicroservicioAsignacionCalendario.Application.DTOs.SesionEntrenamiento;
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
        private readonly string _urlBase = "http://localhost:5097";

        public PlanEntrenamientoClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PlanEntrenamientoResponse> ObtenerPlanEntrenamiento(Guid id, CancellationToken ct)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<PlanEntrenamientoResponse>(
                    $"{_urlBase}/api/TrainingPlan/{id}",
                    ct);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error al obtener un plan de entrenamiento: {ex.Message}");
                return null;
            }
        }

        public async Task<SesionEntrenamientoResponse> ObtenerSesionEntrenamiento(Guid id, CancellationToken ct)
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<SesionEntrenamientoResponse>(
                    $"{_urlBase}/api/TrainingSession/{id}",
                   ct);
                Console.WriteLine(result);
                return result;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error al obtener una sesion de entrenamiento: {ex.Message}");
                return null;
            }
        }

        public async Task<EjercicioSesionResponse> ObtenerEjercicioSesion(Guid idEjercicioSesion, CancellationToken ct)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<EjercicioSesionResponse>(
                    $"{_urlBase}/api/ExerciseSession/{idEjercicioSesion}",
                   ct);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error al obtener una sesion de ejercicio: {ex.Message}");
                return null;
            }
        }

        public async Task<EjercicioResponse> ObtenerEjercicio(Guid idEjercicio, CancellationToken ct)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<EjercicioResponse>(
                    $"{_urlBase}/api/Exercise/{idEjercicio}",
                   ct);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error al obtener un ejercicio: {ex.Message}");
                return null;
            }
        }


    }
}
