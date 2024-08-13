namespace Application.Core.Models.BlazorDTO.User;

public readonly record struct LogInUserData
{
    public required string Username { get; init; }
    public required string Password { get; init; }
}
