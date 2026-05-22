using System;

namespace FitAura.Models
{
    /// <summary>
    /// Reprezentuje dane użytkownika.
    /// </summary>
    /// <param name="Id">Unikalny identyfikator użytkownika.</param>
    /// <param name="Email">Adres e-mail użytkownika.</param>
    /// <param name="Firstname">Imię użytkownika.</param>
    /// <param name="Lastname">Nazwisko użytkownika.</param>
    /// <param name="BirthDate">Data urodzenia użytkownika.</param>
    /// <param name="Weight">Aktualna masa ciała użytkownika.</param>
    /// <param name="Height">Wzrost użytkownika.</param>
    /// <param name="Password">Zaszyfrowane hasło użytkownika.</param>
    /// <param name="Sex">Płeć użytkownika.</param>
    public record User(
        int Id,
        string Email,
        string Firstname,
        string Lastname,
        DateOnly BirthDate,
        decimal Weight,
        decimal Height,
        string Password,
        string Sex);
}