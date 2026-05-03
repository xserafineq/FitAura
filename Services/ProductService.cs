using System;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using FitAura.Models;

namespace FitAura.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Product?> GetProductAsync(string barcodeToFetch)
        {
            if (string.IsNullOrWhiteSpace(barcodeToFetch)) return null;

            try
            {
                var response = await _httpClient.GetStringAsync($"api/v2/product/{barcodeToFetch}.json");
                var node = JsonNode.Parse(response);
                var p = node?["product"];
                var n = p?["nutriments"];

                if (p == null) return null;

                return new Product(
                    name: p?["product_name"]?.ToString() ?? "Brak nazwy",
                    grammage: p?["quantity"]?.ToString() ?? "Brak danych",
                    barCode: node?["code"]?.ToString() ?? barcodeToFetch,
                    kcal: (double?)n?["energy-kcal_100g"] ?? 0.0,
                    protein: (int)Math.Round((double?)n?["proteins_100g"] ?? 0.0),
                    fats: (int)Math.Round((double?)n?["fat_100g"] ?? 0.0),
                    carbs: (int)Math.Round((double?)n?["carbohydrates_100g"] ?? 0.0),
                    imageUrl: p?["image_front_url"]?.ToString()
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd pobierania: {ex.Message}");
                return null;
            }
        }
    }
}