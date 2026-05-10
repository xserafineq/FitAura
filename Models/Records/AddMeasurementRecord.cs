using FitAura.Components.Modals;
using FitAuraApi.Models;

namespace FitAura.Models.Records
{
    public record AddMeasurementRecord(
        decimal Weight,
        decimal Pulse,
        decimal Sugar,
        int DayId
    );
}