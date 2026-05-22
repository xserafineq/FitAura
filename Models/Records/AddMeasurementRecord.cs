using FitAura.Components.Modals;
using FitAuraApi.Models;

namespace FitAura.Models.Records
{
    /// <summary>
    /// Reprezentuje obiekt danych (DTO) służący do tworzenia nowego pomiaru ciała.
    /// </summary>
    /// <param name="Weight">Masa ciała użytkownika.</param>
    /// <param name="Pulse">Tętno użytkownika.</param>
    /// <param name="Sugar">Poziom cukru we krwi użytkownika.</param>
    /// <param name="DayId">Identyfikator dnia.</param>
    public record AddMeasurementRecord(
        decimal Weight,
        decimal Pulse,
        decimal Sugar,
        int DayId
    );
}