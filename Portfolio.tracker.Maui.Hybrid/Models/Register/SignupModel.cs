namespace Portfolio.tracker.Maui.Hybrid.Models.Register;

internal class SignupModel
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public bool AcceptTC { get; set; }
}
