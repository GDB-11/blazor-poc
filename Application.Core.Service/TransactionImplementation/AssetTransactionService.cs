using Application.Core.Contract.TransactionInterface;
using Application.Core.Models.BlazorDTO.AssetTransaction;
using Application.Core.Models.Constants.Messages;
using Application.Core.Models.Database.Transactions;
using Application.Core.Service.Queries;
using FluentResults;
using Gian.Basic.Helper;
using Infrastructure.Core.IDatabase;
using LiteDB;

namespace Application.Core.Service.TransactionImplementation;

public sealed class AssetTransactionService : IAssetTransaction
{
    private readonly ILiteDbGenericRepository<Transaction> _transactionRepository;

    public AssetTransactionService(ILiteDbGenericRepository<Transaction> transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public Result Create(CreateTransaction request)
    {
        Transaction transaction = new()
        {
            TransactionId = GuidHelper.GenerateGuid(),
            PortfolioId = request.PortfolioId,
            PortfolioItemId = request.PortfolioItemId,
            AssetId = request.AssetId,
            OperationType = request.OperationType,
            Price = request.Price,
            Quantity = request.Quantity,
            TransactionFeeAssetId = request.TransactionFeeAssetId,
            TransactionFeeQuantity = request.TransactionFeeQuantity,
            TransactionFeeAssetPrice = request.TransactionFeeAssetPrice,            
            TransactionDateTime = request.TransactionDateTime,
            TransactionHash = request.TransactionHash,
            TransactionUrl = request.TransactionUrl,
            CreationDate = DateTime.UtcNow
        };

        Result result = _transactionRepository.Insert(transaction);

        return result.BuildNewResult(Codes.TransactionCodes.InsertionError, Codes.TransactionCodes.InsertionSuccess);
    }

    public Result Edit(EditTransaction request)
    {
        BsonDocument parameters = new()
        {
            ["TransactionId"] = request.TransactionId,
            ["OperationType"] = new BsonValue((int)request.OperationType),
            ["Price"] = request.Price,
            ["Quantity"] = request.Quantity,
            ["TransactionFeeAssetId"] = request.TransactionFeeAssetId,
            ["TransactionFeeQuantity"] = request.TransactionFeeQuantity,
            ["TransactionFeeAssetPrice"] = request.TransactionFeeAssetPrice,
            ["TransactionDateTime"] = request.TransactionDateTime,
            ["TransactionHash"] = request.TransactionHash,
            ["TransactionUrl"] = request.TransactionUrl,
            ["UpdateDate"] = DateTime.UtcNow
        };

        Result result = _transactionRepository.ExecuteNonQuery(TransactionQueries.UpdateTransactionByTransactionId, parameters);

        return result.BuildNewResult(Codes.TransactionCodes.UpdateError, Codes.TransactionCodes.UpdateSuccess);
    }

    public Result Delete(string transactionId)
    {
        BsonDocument parameters = new()
        {
            ["TransactionId"] = transactionId
        };

        Result result = _transactionRepository.ExecuteNonQuery(TransactionQueries.DeleteTransactionByTransactionId, parameters);

        return result.BuildNewResult(Codes.TransactionCodes.DeletionError, Codes.TransactionCodes.DeletionSuccess);
    }

    public Result DeleteAllFromPortfolioItem(string portfolioItemId)
    {
        BsonDocument parameters = new()
        {
            ["PortfolioItemId"] = portfolioItemId
        };

        Result result = _transactionRepository.ExecuteNonQuery(TransactionQueries.DeleteTransactionsByPortfolioItemId, parameters);

        return result.BuildNewResult(Codes.TransactionCodes.PortfolioItemsTransactionsDeletionError, Codes.TransactionCodes.PortfolioItemsTransactionsDeletionSuccess);
    }

    public Result DeleteAllFromPortfolio(string portfolioId)
    {
        BsonDocument parameters = new()
        {
            ["PortfolioId"] = portfolioId
        };

        Result result = _transactionRepository.ExecuteNonQuery(TransactionQueries.DeleteTransactionsByPortfolioId, parameters);

        return result.BuildNewResult(Codes.TransactionCodes.PortfolioTransactionsDeletionError, Codes.TransactionCodes.PortfolioTransactionsDeletionSuccess);
    }
}
