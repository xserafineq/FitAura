using FitAura.Components.Cards;
using FitAura.Models.Records;
using FitAuraApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitAura.Models
{
    /// <summary>
    /// Reprezentuje dzienny zapis aktywności, odżywiania i pomiarów użytkownika.
    /// </summary>
    /// <remarks>
    /// Klasa pełni rolę modelu danych, który agreguje informacje o zdrowiu i aktywności 
    /// użytkownika w ramach konkretnej daty kalendarzowej.
    /// </remarks>
    public class Day
    {
        /// <summary>
        /// Unikalny identyfikator zapisu w bazie danych.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Łączna liczba spalonych kalorii w ciągu dnia.
        /// </summary>
        public decimal KcalBurned { get; set; }

        /// <summary>
        /// Subiektywny lub obliczony poziom jakości snu (skala np. 1-10).
        /// </summary>
        public int SleepLevel { get; set; }

        /// <summary>
        /// Liczba kroków wykonanych w ciągu dnia.
        /// </summary>
        public int Steps { get; set; }

        /// <summary>
        /// Data, której dotyczy wpis.
        /// </summary>
        public DateOnly Date { get; set; }

        /// <summary>
        /// Identyfikator użytkownika, do którego przypisany jest ten dzień.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Lista pomiarów ciała (np. waga, obwody) dodanych w danym dniu.
        /// </summary>
        public ICollection<AddMeasurementRecord> Measurements { get; set; } = new List<AddMeasurementRecord>();

        /// <summary>
        /// Lista spożytych posiłków zarejestrowanych w danym dniu.
        /// </summary>
        public ICollection<AddMealRecord> Meals { get; set; } = new List<AddMealRecord>();

        /// <summary>
        /// Inicjalizuje nową, pustą instancję klasy.
        /// </summary>
        public Day() { }

        /// <summary>
        /// Inicjalizuje nową instancję klasy z pełnym zestawem danych.
        /// </summary>
        /// <param name="id">Unikalny identyfikator.</param>
        /// <param name="kcalBurned">Ilość spalonych kalorii.</param>
        /// <param name="sleepLevel">Poziom jakości snu.</param>
        /// <param name="steps">Liczba wykonanych kroków.</param>
        /// <param name="date">Data wpisu.</param>
        /// <param name="userId">ID użytkownika.</param>
        /// <param name="measurements">Lista pomiarów zdrowotnych.</param>
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