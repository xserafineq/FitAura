using FitAura.Components.Cards;
using FitAura.Models.Records;
using FitAuraApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitAura.Models
{
    public class Day
    {
        public int Id { get; set; }
        public decimal KcalBurned { get; set; }
        public int SleepLevel { get; set; }
        public int Steps { get; set; }
        public DateOnly Date { get; set; }
        public int UserId { get; set; }

        public ICollection<AddMeasurementRecord> Measurements { get; set; } = new List<AddMeasurementRecord>();

        public Day() { }

        public Day(int id, decimal kcalBurned, int sleepLevel, int steps, DateOnly date, int userId, ICollection<AddMeasurementRecord> measurements)
        {
            Id = id;
            KcalBurned = kcalBurned;
            SleepLevel = sleepLevel;
            Steps = steps;
            Date = date;
            UserId = userId;
            Measurements = measurements;
        }
    }
}