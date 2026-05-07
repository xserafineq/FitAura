using System;
using System.Linq;
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

            
                string quantityStr = p?["quantity"]?.ToString() ?? "Brak danych";
                var (weightOrVolume, unit) = ParseQuantity(quantityStr);

        
                double multiplier = 1.0;
                switch (unit)
                {
                    case "kg":
                        multiplier = 10.0;
                        break;
                    case "g":
                    case "gr":
                    case "ml":
                        multiplier = weightOrVolume / 100.0;
                        break;
                    case "l":
                        multiplier = 10.0; 
                        break;
                    default:
                        multiplier = 1.0;
                        break;
                }

                
                double kcal100 = (double?)n?["energy-kcal_100g"] ?? (double?)n?["energy-kcal"] ?? 0.0;
                double protein100 = (double?)n?["proteins_100g"] ?? 0.0;
                double fat100 = (double?)n?["fat_100g"] ?? 0.0;
                double carbs100 = (double?)n?["carbohydrates_100g"] ?? 0.0;

                return new Product(
                    name: p?["product_name"]?.ToString() ?? "Brak nazwy",
                    grammage: quantityStr,
                    barCode: node?["code"]?.ToString() ?? barcodeToFetch,
                    kcal: Math.Round(kcal100 * multiplier, 2),
                    protein: (int)Math.Round(protein100 * multiplier),
                    fats: (int)Math.Round(fat100 * multiplier),
                    carbs: (int)Math.Round(carbs100 * multiplier),
                    imageUrl: p?["image_front_url"]?.ToString(),
                    market: p?["stores"]?.ToString() ?? "Brak danych"
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd pobierania: {ex.Message}");
                return null;
            }
        }

        public (double Value, string Unit) ParseQuantity(string quantityString)
        {
            if (string.IsNullOrWhiteSpace(quantityString)) return (100, "g");

            string clean = quantityString.ToLower().Replace(" ", "");

            int i = 0;
            while (i < clean.Length && (char.IsDigit(clean[i]) || clean[i] == ',' || clean[i] == '.'))
            {
                i++;
            }

            string valueStr = clean.Substring(0, i).Replace(',', '.');
            string unitStr = clean.Substring(i);

            if (double.TryParse(valueStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double value))
            {
                return (value, unitStr);
            }

            return (100, "g");
        }
    }
}