using Application.Core.Models.BlazorDTO.User;
using FluentResults;

namespace Application.Core.Contract.UserInterface;

public interface IUser
{
    Task<Result> CreateAsync(RegisteredUserData data);
    Task<Result<LoggedInUserData>> LogInAsync(LogInUserData data);
    Task<Result> DoesUsernameExistAsync(string username);
    Task<Result> DoesEmailExistAsync(string email);
}
