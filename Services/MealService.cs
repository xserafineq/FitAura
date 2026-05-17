using FitAura.Components.Cards;
using FitAura.Models;
using FitAura.Models.Records;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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

        public async Task<List<Meal>?> GetMealByDayId(int id)
        {
            try
            {
                var meals = await _httpClient.GetFromJsonAsync<List<Meal>>($"/api/Meals/{id}");
                return meals;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
                return null;
            }
        }

        public async Task<ProductName> GetProductName(int? id)
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<ProductName>($"/api/Products/name/{id}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
                return null;
            }
        }

        public async Task<FullMakro> CalculateTotalMacros(List<MealItem> mealItems)
        {
            try
            {
                // Tworzymy listę zadań do wykonania równolegle
                var tasks = mealItems
                    .Where(item => (item.Type == "product" && item.ProductId.HasValue) || (item.Type == "recipe" && item.RecipeId.HasValue))
                    .Select(async item =>
                    {
                        string url = item.Type == "product" 
                            ? $"/api/Products/calculate/{item.ProductId}/amount/{item.Amount}"
                            : $"/api/Recipes/calculate/{item.RecipeId}/portions/{item.Amount}";

                        var response = await _httpClient.GetAsync(url);
                        if (response.IsSuccessStatusCode)
                        {
                            var data = await response.Content.ReadFromJsonAsync<ProductMakro>();
                            return new FullMakro(data?.Kcal ?? 0, data?.Protein ?? 0, data?.Fats ?? 0, data?.Carbs ?? 0);
                        }
                        return new FullMakro(0, 0, 0, 0);
                    });

                // Czekamy, aż wszystkie zapytania się zakończą
                var results = await Task.WhenAll(tasks);

                return new FullMakro(
                    results.Sum(r => r.Kcal),
                    results.Sum(r => r.Protein),
                    results.Sum(r => r.Fats),
                    results.Sum(r => r.Carbs)
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas obliczeń: {ex.Message}");
                return new FullMakro(0, 0, 0, 0); // W razie błędu zwracamy 0, żeby nie "zamrozić" UI
            }
        }
        public async Task<FullMakro?> CalculateProductMacros(int id, int amount)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<FullMakro>($"/api/Products/calculate/{id}/amount/{amount}");
            }
            catch { return null; }
        }

        public async Task<FullMakro?> CalculateRecipeMacros(int id, int portions)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<FullMakro>($"/api/Recipes/calculate/{id}/portions/{portions}");
            }
            catch { return null; }
        }
    }

    public record FullMakro(int Kcal, int Protein, int Fats, int Carbs);
}
