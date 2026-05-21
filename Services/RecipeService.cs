using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

namespace FitAura.Services
{
    public class RecipeService
    {
        private readonly HttpClient _httpClient;

        public RecipeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

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

        public async Task<List<RecipeSummaryDto>> GetRecipesAsync(string? search = null, int? maxKcal = null, int? categoryId = null, int? userId = null)
        {
            try
            {
                var url = "/api/Recipes";
                var queryParams = new List<string>();
                if (!string.IsNullOrWhiteSpace(search))
                {
                    queryParams.Add($"search={Uri.EscapeDataString(search)}");
                }
                if (maxKcal.HasValue)
                {
                    queryParams.Add($"maxKcal={maxKcal.Value}");
                }
                if (categoryId.HasValue)
                {
                    queryParams.Add($"categoryId={categoryId.Value}");
                }
                if (userId.HasValue)
                {
                    queryParams.Add($"userId={userId.Value}");
                }

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

        private class UploadResponse
        {
            public string ImageUrl { get; set; } = "";
        }

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
    }

    public record CreateRecipeDto(string Name, string? ImageUrl, List<string> Steps, List<CreateRecipeIngredientDto> Ingredients, int Portions, int UserId, List<int> CategoryIds);
    public record CreateRecipeIngredientDto(int ProductId, int Amount, string Unit);
    public record RecipeSummaryDto(int Id, string Name, string? ImageUrl, int TotalKcal, int TotalProtein, int TotalCarbs, int TotalFats, int Portions, List<string> Categories);

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
        List<string> Categories
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
