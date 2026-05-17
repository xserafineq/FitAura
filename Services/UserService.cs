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

        public async Task<User> LoginAsync(string email, string password)
        {
            var requestData = new { Email = email, Password = password };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Users/login", requestData);

                if (response.IsSuccessStatusCode)
                {
                    var user = await response.Content.ReadFromJsonAsync<User>();
                    return user;
                }

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Błędne dane logowania.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas łączenia z API: {ex.Message}");
            }

            return null;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Users/{user.Id}", user);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas aktualizacji użytkownika: {ex.Message}");
                return false;
            }
        }
    }
}