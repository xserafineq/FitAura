using FitAura.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FitAura.Services
{
    public class CategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CategoryModel>> GetCategoriesAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<CategoryModel>>("/api/Categories") ?? new List<CategoryModel>();
            }
            catch
            {
                return new List<CategoryModel>();
            }
        }
    }
}
