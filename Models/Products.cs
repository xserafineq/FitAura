using System;
using System.Collections.Generic;
using System.Text;

namespace FitAura.Models
{
    public record Product(int Id, string Barcode, string Name, int Kcal, int Protein, int Fats, string Unit, int Carbs, string? Store, string? ImageUrl)
    {
    }
}
