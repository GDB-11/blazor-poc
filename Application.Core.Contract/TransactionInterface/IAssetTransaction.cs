using Application.Core.Models.BlazorDTO.AssetTransaction;
using FluentResults;

namespace Application.Core.Contract.TransactionInterface;

public interface IAssetTransaction
{
    Result Create(CreateTransaction request);
    Result Edit(EditTransaction request);
    Result Delete(string transactionId);
    Result DeleteAllFromPortfolioItem(string portfolioItemId);
    Result DeleteAllFromPortfolio(string portfolioId);
}
