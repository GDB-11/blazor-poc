namespace Application.Core.Service.Queries;

internal static class UserQueries
{
    internal const string UsernameValidation =
        @"SELECT
            CASE WHEN EXISTS (SELECT 1 FROM User WHERE Username = @Username)
            THEN 1
            ELSE 0
        END;";

    internal const string EmailValidation =
        @"SELECT
            CASE WHEN EXISTS (SELECT 1 FROM User WHERE Email = @Email)
            THEN 1
            ELSE 0
        END;";

    internal const string FindUserIdByUsername = 
        @"SELECT
	        UserId
        FROM
	        User
        WHERE
	        Username = @Username";

    internal const string GetPasswordByUserId =
        @"SELECT
	        Password
        FROM
	        User
        WHERE
	        UserId = @UserId";

    internal const string GetBasicUserDataByUserId =
        @"SELECT
	        UserId
	        , Username
	        , Email
	        , Status
	        , ProfilePicture
        FROM
	        User
        WHERE
	        UserId = @UserId";
}
