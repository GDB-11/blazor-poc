using FluentResults;
using LiteDB;
using System.Linq.Expressions;

namespace Infrastructure.Core.IDatabase;

public interface ILiteDbGenericRepository<T> where T : class
{
    Result Insert(T entity);
    Result Update(T entity);
    Result Delete(BsonValue id);
    Result<T> GetById(BsonValue id);
    Result<IEnumerable<T>> GetAll();
    Result<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
    Result<IEnumerable<T>> GetCollectionFromCustomQuery(string query, BsonDocument parameters);
    Result<T?> GetFirstFromCustomQuery(string query, BsonDocument parameters);
    Result ExecuteNonQuery(string query, BsonDocument parameters);
}
