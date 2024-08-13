namespace Application.Core.Models.BlazorDTO.User;

public readonly record struct RegisteredUserData
{
    public required string Username { get; init; }
    public required string Password { get; init; }
    public required string Email { get; init; }
}
