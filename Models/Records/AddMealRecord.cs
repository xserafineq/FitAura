using System;
using System.Collections.Generic;
using System.Text;

namespace FitAura.Models.Records
{
    public record AddMealRecord(string Name, int Kcal, int DayId, ICollection<MealItem> MealItems);

}
