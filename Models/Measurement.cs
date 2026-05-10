using FitAura.Models;
using System;
using System.Collections.Generic;

namespace FitAuraApi.Models;

public class Measurement
{
    public decimal Weight { get; set; }

    public decimal Pulse { get; set; }

    public decimal Sugar { get; set; }

    public int DayId { get; set; }

    public Measurement(decimal Weight, decimal Pulse, decimal Sugar, int DayId, ICollection<Measurement> Measurements)
    {
        this.Weight = Weight;
        this.Pulse = Pulse;
        this.Sugar = Sugar;
        this.DayId = DayId;
    }
}
