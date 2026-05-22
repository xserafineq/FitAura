using FitAura.Models;
using FitAura.Models.Records;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FitAura.Services
{
    /// <summary>
    /// Serwis obsługujący komunikację z API w zakresie zarządzania danymi dziennymi użytkownika.
    /// </summary>
    /// <remarks>
    /// Odpowiada za operacje pobierania, tworzenia oraz aktualizacji stanu dnia.
    /// Zintegrowany z <see cref="DayState"/> w celu synchronizacji danych w aplikacji.
    /// </remarks>
    public class DayService
    {
        private readonly DayState _dayState;
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Inicjalizuje nową instancję serwisu.
        /// </summary>
        /// <param name="httpClient">Klient HTTP do komunikacji z API.</param>
        /// <param name="dayState">Stan aplikacji przechowujący bieżący dzień.</param>
        public DayService(HttpClient httpClient, DayState dayState)
        {
            _httpClient = httpClient;
            _dayState = dayState;
        }

        /// <summary>
        /// Pobiera istniejący dzień (<see cref="Day"/>) dla użytkownika lub tworzy nowy, jeśli nie istnieje.
        /// </summary>
        /// <param name="userId">Identyfikator użytkownika.</param>
        /// <returns>Zadanie asynchroniczne reprezentujące operację utworzenia lub pobrania dnia.</returns>
        public async Task CreateDay(int userId)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/Days/get-or-create", new { userId });
                if (response.IsSuccessStatusCode)
                {
                    var day = await response.Content.ReadFromJsonAsync<Day>();

                    if (day != null)
                    {
                        _dayState.SetDay(day);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas łączenia z API: {ex.Message}");
            }
        }

        /// <summary>
        /// Wysyła żądanie aktualizacji danych dnia do serwera.
        /// </summary>
        /// <param name="day">Obiekt danych (<see cref="EditDay"/>) do zaktualizowania.</param>
        /// <returns>Zadanie asynchroniczne reprezentujące operację aktualizacji dnia.</returns>
        public async Task EditDay(EditDay day)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"/api/Days/{day.Id}", day);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Nie udało się zaktualizować dnia. Status: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas łączenia z API: {ex.Message}");
            }
        }

        /// <summary>
        /// Pobiera pełną historię dni (<see cref="Day"/>) zarejestrowanych dla wskazanego użytkownika.
        /// </summary>
        /// <param name="userId">Identyfikator użytkownika.</param>
        /// <returns>Zadanie asynchroniczne zwracające listę obiektów <see cref="Day"/> stanowiącą historię użytkownika.</returns>
        public async Task<List<Day>> GetUserDaysAsync(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/Days/user/{userId}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<Day>>() ?? new List<Day>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas pobierania historii dni użytkownika: {ex.Message}");
            }
            return new List<Day>();
        }
    }
}