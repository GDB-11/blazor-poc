using FluentResults;

namespace Gian.Basic.Helper;

public static class ResultHelper
{
    public static Result<T> OkIf<T>(bool isSuccess, string error, T successValue)
    {
        if (!isSuccess)
        {
            return Result.Fail(error);
        }

        return Result.Ok(successValue);
    }

    public static Result<T> BuildResult<T>(this Result<T> result, string errorMessage, string successMessage)
    {
        if (result.IsFailed)
        {
            return result.WithError(errorMessage);
        }

        return result.WithSuccess(successMessage);
    }

    public static Result BuildResult(this Result result, string errorMessage, string successMessage)
    {
        if (result.IsFailed)
        {
            return result.WithError(errorMessage);
        }

        return result.WithSuccess(successMessage);
    }

    public static Result BuildNewResult(this Result result, string errorMessage, string successMessage)
    {
        if (result.IsFailed)
        {
            return Result.Fail(errorMessage);
        }

        return Result.Ok().WithSuccess(successMessage);
    }

    public static string GetFirstSuccess(this Result result)
    {
        return result.Successes.FirstOrDefault()?.Message ?? string.Empty;
    }

    public static string GetFirstSuccess<T>(this Result<T> result)
    {
        return result.Successes.FirstOrDefault()?.Message ?? string.Empty;
    }

    public static string GetFirstError(this Result result)
    {
        return result.Errors.FirstOrDefault()?.Message ?? string.Empty;
    }

    public static string GetFirstError<T>(this Result<T> result)
    {
        return result.Errors.FirstOrDefault()?.Message ?? string.Empty;
    }
}
