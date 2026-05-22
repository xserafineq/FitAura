using FitAura.Models;
using FitAura.Models.Records;
using FitAuraApi.Models;
/// <summary>
/// Serwis przechowujący stan bieżącego
/// </summary>
/// <remarks>
/// Klasa wykorzystuje wzorzec obserwatora. Komponenty UI powinny subskrybować zdarzenie OnChange,
/// aby reagować na aktualizacje danych w czasie rzeczywistym.
/// </remarks>
public class DayState
{
    /// <summary>
    /// Pobiera lub ustawia obiekt reprezentujący aktualnie edytowany dzień.
    /// </summary>
    public Day? CurrentDay { get; set; }

    /// <summary>
    /// Zdarzenie wywoływane po każdej modyfikacji stanu dnia.
    /// </summary>
    public event Action? OnChange;

    /// <summary>
    /// Inicjalizuje lub zmienia bieżący dzień w serwisie.
    /// </summary>
    /// <param name="day">Obiekt dnia do przypisania.</param>
    public void SetDay(Day day)
    {
        CurrentDay = day;
        OnChange?.Invoke();
    }

    /// <summary>
    /// Aktualizuje liczbę spalonych kalorii i powiadamia subskrybentów o zmianie.
    /// </summary>
    /// <param name="kcalBurned">Nowa wartość spalonych kalorii.</param>
    public void SetBurnedKcals(decimal kcalBurned)
    {
        if (CurrentDay != null)
        {
            CurrentDay.KcalBurned = kcalBurned;
            OnChange?.Invoke();
        }
    }

    /// <summary>
    /// Aktualizuje liczbę wykonanych kroków.
    /// </summary>
    /// <param name="steps">Liczba kroków.</param>
    public void SetSteps(int steps)
    {
        if (CurrentDay != null)
        {
            CurrentDay.Steps = steps;
            OnChange?.Invoke();
        }
    }

    /// <summary>
    /// Aktualizuje ocenę jakości snu
    /// </summary>
    /// <param name="sleepLevel">Poziom oceny jakości snu</param>
    public void SetSleepLevel(int sleepLevel)
    {
        if (CurrentDay != null)
        {
            CurrentDay.SleepLevel = sleepLevel;
            OnChange?.Invoke();
        }
    }

    /// <summary>
    /// Dodaje nowy pomiar do listy pomiarów
    /// </summary>
    /// <param name="measurement">Obiekt pomiaru</param>
    public void SetMeasurement(AddMeasurementRecord measurement)
    {
        if (CurrentDay != null)
        {
            CurrentDay.Measurements.Add(measurement);
            OnChange?.Invoke();
        }
    }

    /// <summary>
    /// Dodaje nowy posiłek do rejestru bieżącego dnia.
    /// </summary>
    /// <param name="meal">Obiekt posiłku do dodania.</param>
    public void SetMeal(AddMealRecord meal)
    {
        if (CurrentDay != null)
        {
            CurrentDay.Meals.Add(meal);
            OnChange?.Invoke();
        }
    }

    /// <summary>
    /// Wymusza ręczne powiadomienie subskrybentów o zmianie stanu.
    /// </summary>
    public void NotifyChange()
    {
        OnChange?.Invoke();
    }
}