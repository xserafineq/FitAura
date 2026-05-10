using FitAura.Components.Cards;
using FitAura.Models.Records;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace FitAura.Services
{
    internal class MealService
    {
        private readonly HttpClient _httpClient;

        public MealService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AddMeal(AddMealRecord meal)
        {

            try
            {
 
                var response = await _httpClient.PostAsJsonAsync("/api/Meals", meal);
                if (response.IsSuccessStatusCode)
                {
                    var createdMeal = await response.Content.ReadFromJsonAsync<Meal>();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Błąd: {response.StatusCode} - {error}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wyjątek: {ex.Message}");
            }
        }
    }
}
