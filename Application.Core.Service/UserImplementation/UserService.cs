using Application.Core.Contract.UserInterface;
using Application.Core.Models.BlazorDTO.User;
using Application.Core.Models.Constants.Messages;
using Application.Core.Models.Constants.User;
using Application.Core.Models.Database.User;
using Application.Core.Service.Queries;
using FluentResults;
using Gian.Basic.Helper;
using Infrastructure.Core.IDatabase;

namespace Application.Core.Service.UserImplementation;

public sealed class UserService : IUser
{
    private readonly IGenericRepository<User> _userRepository;

    public UserService(IGenericRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result> CreateAsync(RegisteredUserData data)
    {
        User newUser = new()
        {
            UserId = GuidHelper.GenerateGuid(),
            Username = data.Username,
            Password = EncryptionHelper.EncryptWithMAC(data.Password),
            Email = data.Email,
            Status = Status.Code.Active,
            CreatedDate = DateTime.UtcNow
        };

        Result<int> result = await _userRepository.InsertAsync(newUser);

        return result.BuildResult(Codes.UserCodes.UserInsertionError, Codes.UserCodes.UserInsertionSuccess).ToResult();
    }

    public async Task<Result<LoggedInUserData>> LogInAsync(LogInUserData data)
    {
        Result<string?> userIdResult = await _userRepository.ExecuteCustomQueryFirstOrDefaultAsync<string?>(UserQueries.FindUserIdByUsername, new { data.Username });

        if (userIdResult.IsFailed)
        {
            return Result.Fail(Codes.GenericDatabaseCodes.GenericDatabaseError);
        }

        if (string.IsNullOrEmpty(userIdResult.Value))
        {
            return Result.Fail(Codes.UserCodes.UserUsernameDoesNotExistError);
        }

        Result<string?> passwordResult = await _userRepository.ExecuteCustomQueryFirstOrDefaultAsync<string?>(UserQueries.GetPasswordByUserId, new { UserId = userIdResult.Value });

        if (passwordResult.IsFailed)
        {
            return Result.Fail(Codes.GenericDatabaseCodes.GenericDatabaseError);
        }

        if (string.IsNullOrEmpty(passwordResult.Value))
        {
            return Result.Fail(Codes.UserCodes.UserPasswordDoesNotExistError);
        }

        if (!data.Password.Equals(EncryptionHelper.DecryptWithMAC(passwordResult.Value), StringComparison.Ordinal))
        {
            return Result.Fail(Codes.UserCodes.UserIncorrectPasswordError);
        }

        Result<LoggedInUserData> userResult = await _userRepository.ExecuteCustomQueryFirstOrDefaultAsync<LoggedInUserData>(UserQueries.GetBasicUserDataByUserId, new { UserId = userIdResult.Value });

        if (userResult.IsFailed)
        {
            return Result.Fail(Codes.GenericDatabaseCodes.GenericDatabaseError);
        }

        return Result.Ok(userResult.Value);
    }

    public async Task<Result> DoesUsernameExistAsync(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            return Result.Fail(Codes.UserCodes.UserUsernameEmptyError);
        }

        Result<int> userValidationResult = await _userRepository.ExecuteCustomQueryFirstOrDefaultAsync<int>(UserQueries.UsernameValidation, new { Username = username });

        if (userValidationResult.IsFailed)
        {
            return Result.Fail(Codes.GenericDatabaseCodes.GenericDatabaseError);
        }

        if (userValidationResult.Value == 1)
        {
            return Result.Fail(Codes.UserCodes.UserUsernameCreationError);
        }

        return Result.Ok();
    }

    public async Task<Result> DoesEmailExistAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return Result.Fail(Codes.UserCodes.UserEmailEmptyError);
        }

        Result<int> userValidationResult = await _userRepository.ExecuteCustomQueryFirstOrDefaultAsync<int>(UserQueries.EmailValidation, new { Email = email });

        if (userValidationResult.IsFailed)
        {
            return Result.Fail(Codes.GenericDatabaseCodes.GenericDatabaseError);
        }

        if (userValidationResult.Value == 1)
        {
            return Result.Fail(Codes.UserCodes.UserEmailCreationError);
        }

        return Result.Ok();
    }
}
