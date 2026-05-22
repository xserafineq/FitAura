using FitAuraApi.Models;
using System.Collections.Generic;

namespace FitAura.Models.Records
{
    /// <summary>
    /// Reprezentuje obiekt danych (DTO) służący do aktualizacji istniejącego wpisu dnia.
    /// </summary>
    /// <param name="Id">Unikalny identyfikator dnia.</param>
    /// <param name="Steps">Całkowita liczba kroków.</param>
    /// <param name="SleepLevel">Ocena jakości snu.</param>
    /// <param name="KcalBurned">Całkowita liczba spalonych kalorii.</param>
    /// <param name="Measurements">Lista pomiarów.</param>
    /// <param name="Meals">Opcjonalna lista posiłków.</param>
    public record EditDay(
        int Id,
        int Steps,
        int SleepLevel,
        decimal KcalBurned,
        ICollection<AddMeasurementRecord> Measurements,
        ICollection<AddMealRecord>? Meals);
}