using System;
using FitAura.Models;

/// <summary>
/// Serwis przechowujący stan zalogowanego użytkownika w aplikacji.
/// </summary>
public class UserState
{
    /// <summary>
    /// Pobiera lub ustawia obiekt aktualnie zalogowanego użytkownika.
    /// </summary>
    public User? CurrentUser { get; set; }

    /// <summary>
    /// Zdarzenie wywoływane każdorazowo po zmianie obiektu użytkownika w serwisie.
    /// </summary>
    public event Action? OnChange;

    /// <summary>
    /// Aktualizuje bieżącego użytkownika i powiadamia wszystkich subskrybentów o zmianie.
    /// </summary>
    /// <param name="user">Obiekt użytkownika</param>
    public void SetUser(User user)
    {
        CurrentUser = user;
        OnChange?.Invoke();
    }
}