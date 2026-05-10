using System;
using System.Collections.Generic;
using System.Text;

namespace FitAura.Models.Records
{
    public record MealItem(string Type, int? ProductId, int? RecipeId);
}
