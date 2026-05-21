using System;
using System.Collections.Generic;
using System.Text;

namespace FitAura.Models
{
    public record User(int Id, string Email, string Firstname, string Lastname, DateOnly BirthDate, decimal Weight, decimal Height, string Password, string Sex);
}

