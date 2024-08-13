using Application.Core.Contract.PortfolioInterface;
using Application.Core.Contract.TransactionInterface;
using Application.Core.Models.BlazorDTO.Portfolio;
using Application.Core.Models.Constants.Messages;
using Application.Core.Models.Constants.Portfolio;
using Application.Core.Models.Database.Portfolio;
using Application.Core.Service.Queries;
using FluentResults;
using Gian.Basic.Helper;
using Infrastructure.Core.IDatabase;

namespace Application.Core.Service.PortfolioImplementation;

public sealed class PortfolioService : IPortfolio
{
    private readonly IGenericRepository<Portfolio> _portfolioRepository;

    private readonly IPortfolioItem _portfolioItem;
    private readonly IAssetTransaction _transaction;

    public PortfolioService(IGenericRepository<Portfolio> portfolioRepository, IPortfolioItem portfolioItem, IAssetTransaction assetTransaction)
    {
        _portfolioRepository = portfolioRepository;
        _portfolioItem = portfolioItem;
        _transaction = assetTransaction;
    }

    public async Task<Result<IEnumerable<UserPortfolio>>> GetAllUserPortfoliosAsync(string userId)
    {
        return await _portfolioRepository.ExecuteCustomQueryAsync<UserPortfolio>(PortfolioQueries.GetUserPortfolios, new { UserId = userId });
    }

    public async Task<Result<UserPortfolio>> GetUserPortfolioAsync(string portfolioId, string userId)
    {
        return await _portfolioRepository.ExecuteCustomQueryFirstOrDefaultAsync<UserPortfolio>(PortfolioQueries.GetUserPortfolio, new { PortfolioId = portfolioId, UserId = userId });
    }

    public async Task<Result> CreateAsync(CreatePortfolio portfolio)
    {
        Portfolio newPortfolio = new()
        {
            PortfolioId = GuidHelper.GenerateGuid(),
            Name = portfolio.Name,
            UserId = portfolio.UserId,
            Status = Status.Code.Active,
            CreatedDate = DateTime.UtcNow
        };

        Result<int> result = await _portfolioRepository.InsertAsync(newPortfolio);

        return result.BuildResult(Codes.PortfolioCodes.PortfolioInsertionError, Codes.PortfolioCodes.PortfolioInsertionSuccess).ToResult();
    }

    public async Task<Result> EditAsync(EditPortfolio portfolio)
    {
        Result<int> result = await _portfolioRepository.ExecuteCustomCommandAsync(PortfolioQueries.UpdateUserPortfolio, new { portfolio.PortfolioId, portfolio.Name, UpdatedDate = DateTime.UtcNow });

        return result.BuildResult(Codes.PortfolioCodes.PortfolioUpdateError, Codes.PortfolioCodes.PortfolioUpdateSuccess).ToResult();
    }

    public async Task<Result> DeleteAsync(string portfolioId)
    {
        Result transactionResult = _transaction.DeleteAllFromPortfolio(portfolioId);

        if (transactionResult.IsFailed)
        {
            return Result.Fail(transactionResult.Errors[0].Message);
        }

        Result<int> portfolioItemResult = await _portfolioItem.DeleteAllPortfolioItemsAsync(portfolioId);

        if (portfolioItemResult.IsFailed)
        {
            return Result.Fail(portfolioItemResult.Errors[0].Message);
        }

        Result<int> result = await _portfolioRepository.ExecuteCustomCommandAsync(PortfolioQueries.DeleteUserPortfolio, new { PortfolioId = portfolioId });

        return result.BuildResult(Codes.PortfolioCodes.PortfolioDeletionError, Codes.PortfolioCodes.PortfolioDeletionSuccess).ToResult();
    }
}
