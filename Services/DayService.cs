using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using FitAura.Models;


namespace FitAura.Services
{
    public class DayService
    {
        private readonly HttpClient _httpClient;

        public DayService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task createDay()
        {
                try
                {
                    var response = await _httpClient.PostAsJsonAsync("/api/Days/get-or-create", new { });
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Dzień został utworzony.");
                    }
                    else
                    {
                        Console.WriteLine($"Nie udało się utworzyć dnia. Status: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Błąd podczas łączenia z API: {ex.Message}");
            }
        }
    }
}