using FitAura.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FitAura.Services
{
    /// <summary>
    /// Serwis obsługujący zapytania do API w celu wyszukiwania i pobierania danych o produktach spożywczych.
    /// </summary>
    /// <remarks>
    /// Odpowiada za pobieranie szczegółowych informacji o produktach na podstawie kodów kreskowych 
    /// oraz wyszukiwanie produktów w bazie danych według frazy.
    /// </remarks>
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Inicjalizuje nową instancję serwisu <see cref="ProductService"/>.
        /// </summary>
        /// <param name="httpClient">Klient HTTP do komunikacji z API.</param>
        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Pobiera produkt z lokalnej bazy danych na podstawie kodu kreskowego.
        /// </summary>
        /// <param name="barcode">Kod kreskowy produktu (np. EAN).</param>
        /// <returns>Obiekt <see cref="Product"/>, jeśli znaleziono; w przeciwnym razie <c>null</c>.</returns>
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

        /// <summary>
        /// Wyszukuje produkty w bazie danych na podstawie podanej frazy.
        /// </summary>
        /// <param name="query">Fraza wyszukiwania (nazwa produktu lub jej fragment).</param>
        /// <returns>Lista znalezionych produktów <see cref="Product"/>; jeśli nie znaleziono lub wystąpił błąd, zwraca pustą listę.</returns>
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