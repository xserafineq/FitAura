using FitAura.Models;
using FitAura.Models.Records;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace FitAura.Services
{
    public class DayService
    {
        private readonly DayState _dayState;

        private readonly HttpClient _httpClient;

        public DayService(HttpClient httpClient, DayState dayState)
        {
            _httpClient = httpClient;
            _dayState = dayState;
        }

        public async Task createDay(int userId)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/Days/get-or-create", new { userId });
                if (response.IsSuccessStatusCode)
                {
                    var day = await response.Content.ReadFromJsonAsync<Day>();

                    if (day != null)
                    {
                       
                        _dayState.SetDay(day);
                        Console.WriteLine($"Dzień został załadowany do stanu. ID: {day.Id}");
                    }
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

        public async Task editDay(EditDay day)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"/api/Days/{day.Id}", day);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Dzień został zaktualizowany.");
                }
                else
                {
                    Console.WriteLine($"Nie udało się zaktualizować dnia. Status: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas łączenia z API: {ex.Message}");
            }
        }

        public async Task<List<Day>> GetUserDaysAsync(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/Days/user/{userId}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<Day>>() ?? new List<Day>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas pobierania historii dni użytkownika: {ex.Message}");
            }
            return new List<Day>();
        }
    }
}