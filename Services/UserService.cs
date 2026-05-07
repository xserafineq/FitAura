using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using FitAura.Models;


namespace FitAura.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            Console.WriteLine($"Próbuję zalogować użytkownika: {email}");

            var requestData = new { Email = email, Password = password };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Users/login", requestData);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<bool>();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas łączenia z API: {ex.Message}");
            }

            return false;
        }
    }
}