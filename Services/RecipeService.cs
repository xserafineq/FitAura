using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using FitAura.Models.Records;

namespace FitAura.Services
{
    /// <summary>
    /// Serwis zarządzający przepisami kulinarnymi, obsługujący przesyłanie plików graficznych 
    /// oraz operacje CRUD na przepisach.
    /// </summary>
    public class RecipeService
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Inicjalizuje nową instancję serwisu <see cref="RecipeService"/>.
        /// </summary>
        public RecipeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Przesyła plik graficzny przepisu na serwer.
        /// </summary>
        /// <param name="file">Plik wybrany przez użytkownika (z komponentu <see cref="InputFile"/>).</param>
        /// <returns>URL do przesłanego obrazu lub <c>null</c> w przypadku błędu.</returns>
        public async Task<string?> UploadImageAsync(IBrowserFile file)
        {
            try
            {
                using var content = new MultipartFormDataContent();

                var fileContent = new StreamContent(file.OpenReadStream(maxAllowedSize: 5120000));
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                content.Add(fileContent, "file", file.Name);

                var response = await _httpClient.PostAsync("/api/Recipes/upload-image", content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<UploadResponse>();
                    return result?.ImageUrl;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Tworzy nowy przepis w systemie.
        /// </summary>
        /// <param name="recipeDto">Obiekt danych zawierający szczegóły przepisu.</param>
        /// <returns><c>true</c> jeśli operacja się powiodła, w przeciwnym razie <c>false</c>.</returns>
        public async Task<bool> CreateRecipeAsync(CreateRecipeDto recipeDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/Recipes", recipeDto);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Pobiera listę przepisów z możliwością filtrowania i sortowania.
        /// </summary>
        /// <param name="search">Fraza wyszukiwania.</param>
        /// <param name="minKcal">Minimalna wartość kaloryczna.</param>
        /// <param name="maxKcal">Maksymalna wartość kaloryczna.</param>
        /// <param name="categoryId">Filtr kategorii.</param>
        /// <param name="userId">Filtr po autorze (ID użytkownika).</param>
        /// <param name="authorEmail">Filtr po e-mailu autora.</param>
        /// <param name="sort">Parametr sortowania.</param>
        /// <returns>Lista podsumowań przepisów w postaci obiektów <see cref="RecipeSummaryDto"/>.</returns>
        public async Task<List<RecipeSummaryDto>> GetRecipesAsync(
            string? search = null,
            int? minKcal = null,
            int? maxKcal = null,
            int? categoryId = null,
            int? userId = null,
            string? authorEmail = null,
            string? sort = null)
        {
            try
            {
                var url = "/api/Recipes";
                var queryParams = new List<string>();

                if (!string.IsNullOrWhiteSpace(search)) queryParams.Add($"search={Uri.EscapeDataString(search)}");
                if (maxKcal.HasValue) queryParams.Add($"maxKcal={maxKcal.Value}");
                if (categoryId.HasValue) queryParams.Add($"categoryId={categoryId.Value}");
                if (userId.HasValue) queryParams.Add($"userId={userId.Value}");
                if (minKcal.HasValue) queryParams.Add($"minKcal={minKcal.Value}");
                if (!string.IsNullOrWhiteSpace(authorEmail)) queryParams.Add($"authorEmail={Uri.EscapeDataString(authorEmail)}");
                if (!string.IsNullOrWhiteSpace(sort)) queryParams.Add($"sort={Uri.EscapeDataString(sort)}");

                if (queryParams.Any())
                {
                    url += "?" + string.Join("&", queryParams);
                }

                return await _httpClient.GetFromJsonAsync<List<RecipeSummaryDto>>(url) ?? new List<RecipeSummaryDto>();
            }
            catch
            {
                return new List<RecipeSummaryDto>();
            }
        }

        /// <summary>
        /// Pobiera szczegółowe informacje o wybranym przepisie.
        /// </summary>
        /// <param name="id">Identyfikator przepisu.</param>
        /// <returns>Obiekt <see cref="RecipeDetailDto"/> ze szczegółami przepisu lub <c>null</c>.</returns>
        public async Task<RecipeDetailDto?> GetRecipeAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<RecipeDetailDto>($"/api/Recipes/{id}");
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Aktualizuje istniejący przepis na podstawie danych <see cref="CreateRecipeDto"/>.
        /// </summary>
        /// <param name="id">Identyfikator przepisu do aktualizacji.</param>
        /// <param name="recipeDto">Nowe dane przepisu.</param>
        /// <returns><c>true</c> jeśli operacja się powiodła, w przeciwnym razie <c>false</c>.</returns>
        public async Task<bool> UpdateRecipeAsync(int id, CreateRecipeDto recipeDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"/api/Recipes/{id}", recipeDto);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        private class UploadResponse
        {
            public string ImageUrl { get; set; } = "";
        }
    }

    public record CreateRecipeDto(string Name, string? ImageUrl, List<string> Steps, List<CreateRecipeIngredientDto> Ingredients, int Portions, int UserId, List<int> CategoryIds);

    public record CreateRecipeIngredientDto(int ProductId, int Amount, string Unit);

    public record RecipeSummaryDto(int Id, string Name, string? ImageUrl, int TotalKcal, int TotalProtein, int TotalCarbs, int TotalFats, int Portions, List<string> Categories, int TotalWeight = 0);



    public record RecipeDetailDto(

        int Id,

        string Name,

        string? ImageUrl,

        List<string> Steps,

        int TotalKcal,

        int TotalProtein,

        int TotalCarbs,

        int TotalFats,

        List<RecipeIngredientDetailDto> Ingredients,

        int Portions,

        List<string> Categories,

        int TotalWeight = 0

    );



    public record RecipeIngredientDetailDto(

        int ProductId,

        string ProductName,

        int Amount,

        string Unit,

        int Kcal,

        int Protein,

        int Carbs,

        int Fats

    );

}





