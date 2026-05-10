using FitAuraApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitAura.Models.Records
{
    public record EditDay(int Id,
        int Steps,
        int SleepLevel,
        decimal KcalBurned,
        ICollection<AddMeasurementRecord> Measurements);
}
