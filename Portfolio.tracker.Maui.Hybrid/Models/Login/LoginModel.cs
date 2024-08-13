namespace Portfolio.tracker.Maui.Hybrid.Models.Login;

internal sealed class LoginModel
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public bool RememberMe { get; set; }
}
