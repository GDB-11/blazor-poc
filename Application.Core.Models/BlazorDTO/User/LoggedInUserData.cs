using Application.Core.Models.Constants.User;

namespace Application.Core.Models.BlazorDTO.User;

public readonly record struct LoggedInUserData
{
    public required string UserId { get; init; }
    public required string Username { get; init; }
    public required string Email { get; init; }
    public Status.Code Status { get; init; }
    public string? ProfilePicture { get; init; }
}
