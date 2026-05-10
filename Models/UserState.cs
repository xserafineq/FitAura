using FitAura.Models;

public class UserState
{
    public User CurrentUser { get; set; }

    public event Action OnChange;
    public void SetUser(User user)
    {
        CurrentUser = user;
        OnChange?.Invoke();
    }
}