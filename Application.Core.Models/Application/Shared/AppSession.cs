using Application.Core.Models.BlazorDTO.User;

namespace Application.Core.Models.Application.Shared;

public sealed class AppSession
{
    public required LoggedInUserData User { get; set; }
    public required DateTime ExpirationDate { get; set; }
}
