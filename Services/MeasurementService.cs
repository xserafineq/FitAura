using FitAura.Components.Modals;
using FitAura.Models;
using FitAura.Models.Records;
using FitAuraApi.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace FitAura.Services
{
    public class MeasurementService
    {

        private readonly HttpClient _httpClient;

        public MeasurementService(HttpClient httpClient, DayState dayState)
        {
            _httpClient = httpClient;
        }

        public async Task AddMeasurement(AddMeasurementRecord measurement) 
        {
            try
            {

                var response = await _httpClient.PostAsJsonAsync("/api/Measurements", measurement);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"API zwróciło błąd: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas łączenia z API: {ex.Message}");
            }
        }

    }
}