using System;
using System.Collections.Generic;
using System.Text;

namespace FitAura.Models.Records
{
    /// <summary>
    /// Reprezentuje obiekt danych (DTO) służący do tworzenia nowego posiłku.
    /// </summary>
    /// <param name="Name">Nazwa posiłku.</param>
    /// <param name="Kcal">Kalorie w posiłku.</param>
    /// <param name="Protein">Białko w posiłku.</param>
    /// <param name="Fats">Tłuszcze w posiłku.</param>
    /// <param name="Carbs">Węglowodany w posiłku.</param>
    /// <param name="DayId">Identyfikator dnia.</param>
    /// <param name="MealItems">Lista składników posiłku.</param>
    /// <param name="Id">Identyfikator posiłku w bazie danych (domyślnie 0).</param>
    public record AddMealRecord(string Name, int Kcal, int Protein, int Fats, int Carbs, int DayId, ICollection<MealItem> MealItems, int Id = 0);

}
