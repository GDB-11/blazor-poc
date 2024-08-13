using FluentResults;

namespace Infrastructure.Core.IDatabase;

public interface IGenericRepository<T> where T : class
{
    Task<Result<IEnumerable<T>>> GetAllAsync();
    Task<Result<T>> GetByIdAsync(object id);
    Task<Result<int>> InsertAsync(T entity);
    Task<Result> UpdateAsync(T entity);
    Task<Result> DeleteAsync(T entity);
    Task<Result<IEnumerable<U>>> ExecuteCustomQueryAsync<U>(string query, object? parameters = null);
    Task<Result<U?>> ExecuteCustomQueryFirstOrDefaultAsync<U>(string query, object? parameters = null);
    Task<Result<int>> ExecuteCustomCommandAsync(string command, object? parameters = null);
    Task<Result> ExecuteInTransactionAsync(Func<Task> action);
}