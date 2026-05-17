using FitAura.Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace FitAura.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<Product?> GetLocalProductAsync(string barcode)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/Products/{barcode}");

                if (response.IsSuccessStatusCode)
                {

                    return await response.Content.ReadFromJsonAsync<Product>();
                }

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"Produkt o kodzie {barcode} nie istnieje w lokalnej bazie.");
                    return null;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd połączenia z lokalnym API: {ex.Message}");
                return null;
            }
        }

        public async Task<List<Product>> SearchProductsAsync(string query)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(query)) return new List<Product>();
                var response = await _httpClient.GetAsync($"/api/Products/search?q={query}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<Product>>() ?? new List<Product>();
                }
                return new List<Product>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas wyszukiwania: {ex.Message}");
                return new List<Product>();
            }
        }

    }
}