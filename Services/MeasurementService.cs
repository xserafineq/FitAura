using FitAura.Models.Records;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FitAura.Services
{
    /// <summary>
    /// Serwis obsługujący operacje związane z zapisywaniem pomiarów zdrowotnych użytkownika.
    /// </summary>
    public class MeasurementService
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Inicjalizuje nową instancję serwisu <see cref="MeasurementService"/>.
        /// </summary>
        /// <param name="httpClient">Klient HTTP używany do komunikacji z API.</param>
        /// <param name="dayState">Stan bieżącego dnia (do ewentualnej synchronizacji w przyszłości).</param>
        public MeasurementService(HttpClient httpClient, DayState dayState)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Wysyła żądanie zapisu nowego pomiaru.
        /// </summary>
        /// <param name="measurement">Obiekt rekordu (<see cref="AddMeasurementRecord"/>) zawierający dane pomiarowe (np. waga, tętno).</param>
        /// <returns>Zadanie asynchroniczne reprezentujące operację.</returns>
        public async Task AddMeasurement(AddMeasurementRecord measurement)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/Measurements", measurement);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"API zwróciło błąd: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas łączenia z API: {ex.Message}");
            }
        }
    }
}