using System;
using System.Collections.Generic;
using System.Text;

namespace FitAura.Models.Records
{
    public record ProductName(int Id, string Name, int Kcal, string Unit, string? ImageUrl);
}
