using FitAura.Models;
using FitAura.Models.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FitAura.Components.Cards;


namespace FitAura.Services
{
    /// <summary>
    /// Serwis odpowiedzialny za komunikację z API w zakresie zarządzania posiłkami 
    /// oraz obliczania wartości odżywczych produktów i przepisów.
    /// </summary>
    internal class MealService
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Inicjalizuje nową instancję serwisu <see cref="MealService"/>.
        /// </summary>
        /// <param name="httpClient">Klient HTTP wykorzystywany do komunikacji z serwerem API.</param>
        public MealService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Wysyła żądanie dodania nowego posiłku.
        /// </summary>
        /// <param name="meal">Obiekt rekordu (<see cref="AddMealRecord"/>) zawierający szczegóły dodawanego posiłku.</param>
        /// <returns>Zadanie asynchroniczne reprezentujące operację dodania posiłku.</returns>
        public async Task AddMeal(AddMealRecord meal)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/Meals", meal);
                if (response.IsSuccessStatusCode)
                {
                    // Sukces - opcjonalnie można obsłużyć zwrócony obiekt createdMeal
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

        /// <summary>
        /// Pobiera listę wszystkich posiłków przypisanych do konkretnego dnia.
        /// </summary>
        /// <param name="id">Identyfikator dnia.</param>
        /// <returns>Lista obiektów <see cref="Meal"/> lub <c>null</c> w przypadku błędu.</returns>
        public async Task<List<Meal>?> GetMealByDayId(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Meal>>($"/api/Meals/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Pobiera nazwę produktu na podstawie jego unikalnego identyfikatora.
        /// </summary>
        /// <param name="id">Identyfikator produktu (może być null).</param>
        /// <returns>Obiekt <see cref="ProductName"/> zawierający nazwę, lub <c>null</c> w razie problemów.</returns>
        public async Task<ProductName?> GetProductName(int? id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ProductName>($"/api/Products/name/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Oblicza łączne makroskładniki dla podanej listy elementów posiłku.
        /// </summary>
        /// <remarks>
        /// Metoda iteruje po liście <paramref name="mealItems"/>, wykonując równoległe zapytania do API 
        /// dla produktów i przepisów, a następnie sumuje uzyskane wyniki.
        /// </remarks>
        /// <param name="mealItems">Lista elementów składowych posiłku.</param>
        /// <returns>Obiekt <see cref="FullMakro"/> z sumarycznymi wartościami odżywczymi.</returns>
        public async Task<FullMakro> CalculateTotalMacros(List<MealItem> mealItems)
        {
            try
            {
                var tasks = mealItems
                    .Where(item => (item.Type == "product" && item.ProductId.HasValue) || (item.Type == "recipe" && item.RecipeId.HasValue))
                    .Select(async item =>
                    {
                        string url = item.Type == "product"
                            ? $"/api/Products/calculate/{item.ProductId}/amount/{item.Amount}"
                            : $"/api/Recipes/calculate/{item.RecipeId}/grams/{item.Amount}";

                        var response = await _httpClient.GetAsync(url);
                        if (response.IsSuccessStatusCode)
                        {
                            var data = await response.Content.ReadFromJsonAsync<ProductMakro>();
                            return new FullMakro(data?.Kcal ?? 0, data?.Protein ?? 0, data?.Fats ?? 0, data?.Carbs ?? 0);
                        }
                        return new FullMakro(0, 0, 0, 0);
                    });

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
                return new FullMakro(0, 0, 0, 0);
            }
        }

        /// <summary>
        /// Oblicza wartości odżywcze konkretnego produktu.
        /// </summary>
        /// <param name="id">Identyfikator produktu.</param>
        /// <param name="amount">Ilość produktu w gramach.</param>
        /// <returns>Obiekt <see cref="FullMakro"/> lub <c>null</c> w przypadku niepowodzenia.</returns>
        public async Task<FullMakro?> CalculateProductMacros(int id, int amount)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<FullMakro>($"/api/Products/calculate/{id}/amount/{amount}");
            }
            catch { return null; }
        }

        /// <summary>
        /// Oblicza wartości odżywcze dla wybranego przepisu.
        /// </summary>
        /// <param name="id">Identyfikator przepisu.</param>
        /// <param name="grams">Masa przepisu w gramach.</param>
        /// <returns>Obiekt <see cref="FullMakro"/> lub <c>null</c> w przypadku niepowodzenia.</returns>
        public async Task<FullMakro?> CalculateRecipeMacros(int id, int grams)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<FullMakro>($"/api/Recipes/calculate/{id}/grams/{grams}");
            }
            catch { return null; }
        }
    }

    /// <summary>
    /// Reprezentuje sumaryczne wartości odżywcze.
    /// </summary>
    /// <param name="Kcal">Liczba kalorii.</param>
    /// <param name="Protein">Zawartość białka.</param>
    /// <param name="Fats">Zawartość tłuszczów.</param>
    /// <param name="Carbs">Zawartość węglowodanów.</param>
    public record FullMakro(int Kcal, int Protein, int Fats, int Carbs);
}