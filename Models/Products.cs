using System;
using System.Collections.Generic;
using System.Text;

namespace FitAura.Models
{
    public record Product(string name,string grammage, string barCode, double kcal, int protein, int fats, int carbs, string? imageUrl, string market);
}
