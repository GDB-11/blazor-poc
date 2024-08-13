using Dapper;
using Dapper.Contrib.Extensions;
using FluentResults;
using Gian.Basic.Helper;
using Infrastructure.Core.IDatabase;
using System.Data;

namespace Infrastructure.Core.Database;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly IDbConnection _dbConnection;

    public GenericRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<Result<IEnumerable<T>>> GetAllAsync()
    {
        try
        {
            IEnumerable<T> result = await _dbConnection.GetAllAsync<T>();

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error("Failed to retrieve all records").CausedBy(ex));
        }
    }

    public async Task<Result<T>> GetByIdAsync(object id)
    {
        try
        {
            T result = await _dbConnection.GetAsync<T>(id);

            return result != null ? Result.Ok(result) : Result.Fail(new Error("Record not found"));
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error("Failed to retrieve record").CausedBy(ex));
        }
    }

    public async Task<Result<int>> InsertAsync(T entity)
    {
        try
        {
            int result = await _dbConnection.InsertAsync(entity);

            return ResultHelper.OkIf(result > 0, "No rows were inserted", result);
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error("Failed to insert record").CausedBy(ex));
        }
    }

    public async Task<Result> UpdateAsync(T entity)
    {
        try
        {
            bool success = await _dbConnection.UpdateAsync(entity);

            return Result.OkIf(success, "No rows were updated");
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error("Failed to update record").CausedBy(ex));
        }
    }

    public async Task<Result> DeleteAsync(T entity)
    {
        try
        {
            bool success = await _dbConnection.DeleteAsync(entity);

            return Result.OkIf(success, "Failed to delete record");
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error("Failed to delete record").CausedBy(ex));
        }
    }

    public async Task<Result<IEnumerable<U>>> ExecuteCustomQueryAsync<U>(string query, object? parameters = null)
    {
        try
        {
            IEnumerable<U> result = await _dbConnection.QueryAsync<U>(query, parameters);

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error("Failed to execute custom query").CausedBy(ex));
        }
    }

    public async Task<Result<U?>> ExecuteCustomQueryFirstOrDefaultAsync<U>(string query, object? parameters = null)
    {
        try
        {
            U? result = await _dbConnection.QueryFirstOrDefaultAsync<U?>(query, parameters);

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error("Failed to execute custom query").CausedBy(ex));
        }
    }

    public async Task<Result<int>> ExecuteCustomCommandAsync(string command, object? parameters = null)
    {
        try
        {
            int result = await _dbConnection.ExecuteAsync(command, parameters);

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error("Failed to execute custom command").CausedBy(ex));
        }
    }

    public async Task<Result> ExecuteInTransactionAsync(Func<Task> action)
    {
        using IDbTransaction transaction = _dbConnection.BeginTransaction();

        try
        {
            await action();
            transaction.Commit();
            return Result.Ok();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            return Result.Fail(new Error("Transaction failed").CausedBy(ex));
        }
    }
}