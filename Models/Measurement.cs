using FitAura.Models;

namespace FitAuraApi.Models;

/// <summary>
/// Pomiar parametrów medycznych (waga, tętno, cukier).
/// </summary>

public class Measurement
{
    /// <summary>
    /// Waga ciała mierzona w kilogramach.
    /// </summary>
    public decimal Weight { get; set; }

    /// <summary>
    /// Tętno użytkownika (uderzenia na minutę).
    /// </summary>
    public decimal Pulse { get; set; }

    /// <summary>
    /// Poziom cukru we krwi.
    /// </summary>
    public decimal Sugar { get; set; }

    /// <summary>
    /// Identyfikator dnia, z którym powiązany jest ten pomiar.
    /// </summary>
    public int DayId { get; set; }

    /// <summary>
    /// Inicjalizuje nową instancję klasy <see cref="Measurement"/> z określonymi wartościami pomiarowymi.
    /// </summary>
    /// <param name="Weight">Waga ciała w kg.</param>
    /// <param name="Pulse">Tętno (BPM).</param>
    /// <param name="Sugar">Poziom cukru.</param>
    /// <param name="DayId">Identyfikator powiązanego dnia.</param>
    public Measurement(decimal Weight, decimal Pulse, decimal Sugar, int DayId)
    {
        this.Weight = Weight;
        this.Pulse = Pulse;
        this.Sugar = Sugar;
        this.DayId = DayId;
    }
}