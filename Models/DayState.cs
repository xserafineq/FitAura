using FitAura.Models;
using FitAura.Models.Records;
using FitAuraApi.Models;

public class DayState
{
    public Day CurrentDay { get; set; }

    public event Action OnChange;
    public void SetDay(Day day)
    {
        CurrentDay = day;
        OnChange?.Invoke();
    }
    public void SetBurnedKcals(decimal kcalBurned)
    {
        if (CurrentDay != null) 
        {
            CurrentDay.KcalBurned = kcalBurned;
            OnChange?.Invoke();
        }
    }

    public void SetSteps(int steps)
    {
        if (CurrentDay != null)
        {
            CurrentDay.Steps = steps;
            OnChange?.Invoke();
        }
    }

    public void SetSleepLevel(int sleepLevel)
    {
        if (CurrentDay != null)
        {
            CurrentDay.SleepLevel = sleepLevel;
            OnChange?.Invoke();
        }
    }

    public void SetMeasurement(AddMeasurementRecord measurement)
    {
        if (CurrentDay != null)
        {
            CurrentDay.Measurements.Add(measurement);
            OnChange?.Invoke();
        }
    }
    public void SetMeal(AddMealRecord meal)
    {
        if (CurrentDay != null)
        {
            CurrentDay.Meals.Add(meal);
            OnChange?.Invoke();
        }
    }

}