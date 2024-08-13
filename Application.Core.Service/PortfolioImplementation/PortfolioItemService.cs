using Application.Core.Contract.PortfolioInterface;
using Application.Core.Contract.TransactionInterface;
using Application.Core.Models.BlazorDTO.PortfolioItem;
using Application.Core.Models.Constants.Messages;
using Application.Core.Models.Constants.Portfolio;
using Application.Core.Models.Database.Asset;
using Application.Core.Models.Database.Portfolio;
using Application.Core.Service.Queries;
using FluentResults;
using Gian.Basic.Helper;
using Infrastructure.Core.IDatabase;

namespace Application.Core.Service.PortfolioImplementation;

public sealed class PortfolioItemService : IPortfolioItem
{
    private readonly IGenericRepository<Asset> _assetRepository;
    private readonly IGenericRepository<PortfolioItem> _portfolioItemRepository;
    private readonly IAssetTransaction _transaction;

    public PortfolioItemService(IGenericRepository<Asset> assetRepository, IGenericRepository<PortfolioItem> portfolioItemRepository, IAssetTransaction assetTransaction)
    {
        _assetRepository = assetRepository;
        _portfolioItemRepository = portfolioItemRepository;
        _transaction = assetTransaction;
    }

    public async Task<Result<IEnumerable<UserPortfolioItem>>> GetAllPortfolioItemsAsync(string portfolioId)
    {
        return await _portfolioItemRepository.ExecuteCustomQueryAsync<UserPortfolioItem>(PortfolioItemQueries.GetPortfolioItemsByPortfolioId, new { PortfolioId = portfolioId });
    }

    public async Task<Result> CreateAsync(CreatePortfolioItem item)
    {
        Asset? asset;

        Result<Asset?> existingAssetResult = await _assetRepository.ExecuteCustomQueryFirstOrDefaultAsync<Asset?>(AssetQueries.GetAssetIdByTicker, new { item.Ticker });

        if (existingAssetResult.IsFailed)
        {
            return Result.Fail(Codes.GenericDatabaseCodes.GenericDatabaseError);
        }

        if (existingAssetResult.Value is null)
        {
            asset = new()
            {
                AssetId = GuidHelper.GenerateGuid(),
                Name = item.Name,
                Ticker = item.Ticker,
                //Logo = item.Logo ?? SvgHelper.GenerateSvg(item.Ticker[0]),
                CreatedDate = DateTime.UtcNow
            };

            Result<int> assetResult = await _assetRepository.InsertAsync(asset);

            if (assetResult.IsFailed)
            {
                return assetResult.ToResult().WithError(Codes.AssetCodes.AssetInsertionError);
            }
        }
        else
        {
            asset = existingAssetResult.Value;
        }
        

        PortfolioItem portfolioItem = new()
        {
            PortfolioItemId = GuidHelper.GenerateGuid(),
            PortfolioId = item.PortfolioId,
            AssetId = asset.AssetId,
            Display = ItemStatus.Code.Visible,
            CreatedDate = DateTime.UtcNow
        };

        Result<int> portfolioItemResult = await _portfolioItemRepository.InsertAsync(portfolioItem);

        return portfolioItemResult.BuildResult(Codes.PortfolioItemCodes.InsertionError, Codes.PortfolioItemCodes.InsertionSuccess).ToResult();
    }

    public async Task<Result> DeleteAsync(string portfolioItemId)
    {
        Result transactionResult = _transaction.DeleteAllFromPortfolioItem(portfolioItemId);

        if (transactionResult.IsFailed)
        {
            return Result.Fail(transactionResult.Errors[0].Message);
        }

        Result<int> result = await _portfolioItemRepository.ExecuteCustomCommandAsync(PortfolioItemQueries.DeletePortfolioItemByPortfolioId, new { PortfolioItemId = portfolioItemId });

        return result.BuildResult(Codes.PortfolioItemCodes.DeletionError, Codes.PortfolioItemCodes.DeletionSuccess).ToResult();
    }

    public async Task<Result> DeleteAllPortfolioItemsAsync(string portfolioId)
    {
        Result<int> result = await _portfolioItemRepository.ExecuteCustomCommandAsync(PortfolioItemQueries.DeletePortfolioItemsByPortfolioId, new { PortfolioId = portfolioId });

        return result.BuildResult(Codes.PortfolioItemCodes.PortfolioItemsDeletionError, Codes.PortfolioItemCodes.PortfolioItemsDeletionSuccess).ToResult();
    }
}
