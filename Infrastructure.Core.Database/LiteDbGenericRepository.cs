using FluentResults;
using Infrastructure.Core.IDatabase;
using LiteDB;
using System.Linq.Expressions;

namespace Infrastructure.Core.Database;

public sealed class LiteDbGenericRepository<T> : ILiteDbGenericRepository<T> where T : class
{
    private readonly LiteDatabase _database;
    private readonly ILiteCollection<T> _collection;

    public LiteDbGenericRepository(string connectionString, string collectionName)
    {
        _database = new LiteDatabase(connectionString);
        _collection = _database.GetCollection<T>(collectionName);
    }

    public Result Insert(T entity)
    {
        try
        {
            _collection.Insert(entity);

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error("Failed to insert record").CausedBy(ex));
        }
    }

    public Result Update(T entity)
    {
        try
        {
            _collection.Update(entity);

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error("Failed to update record").CausedBy(ex));
        }
    }

    public Result Delete(BsonValue id)
    {
        try
        {
            _collection.Delete(id);

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error("Failed to delete record").CausedBy(ex));
        }
    }

    public Result<T> GetById(BsonValue id)
    {
        try
        {
            T entity = _collection.FindById(id);

            return Result.Ok(entity);
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error("Failed to retrieve record by ID").CausedBy(ex));
        }
    }

    public Result<IEnumerable<T>> GetAll()
    {
        try
        {
            IEnumerable<T> result = _collection.FindAll();

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error("Failed to retrieve all records").CausedBy(ex));
        }
    }

    public Result<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
    {
        try
        {
            IEnumerable<T> result = _collection.Find(predicate);

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error("Failed to find records").CausedBy(ex));
        }
    }

    public Result<IEnumerable<T>> GetCollectionFromCustomQuery(string query, BsonDocument parameters)
    {
        try
        {
            IBsonDataReader result = _database.Execute(query, parameters);

            IEnumerable<T> entities = result.ToEnumerable().Select(value => BsonMapper.Global.ToObject<T>(value.AsDocument));

            return Result.Ok(entities);
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error("Failed to execute custom query").CausedBy(ex));
        }
    }

    public Result<T?> GetFirstFromCustomQuery(string query, BsonDocument parameters)
    {
        try
        {
            IBsonDataReader result = _database.Execute(query, parameters);

            T? entities = result.ToEnumerable().Select(value => BsonMapper.Global.ToObject<T>(value.AsDocument)).FirstOrDefault();

            return Result.Ok(entities);
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error("Failed to execute custom query").CausedBy(ex));
        }
    }

    public Result ExecuteNonQuery(string query, BsonDocument parameters)
    {
        try
        {
            IBsonDataReader result = _database.Execute(query, parameters);

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error("Failed to execute non-query").CausedBy(ex));
        }
    }
}
