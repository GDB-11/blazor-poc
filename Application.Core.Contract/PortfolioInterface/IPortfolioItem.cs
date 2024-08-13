using Application.Core.Models.BlazorDTO.PortfolioItem;
using FluentResults;

namespace Application.Core.Contract.PortfolioInterface;

public interface IPortfolioItem
{
    Task<Result<IEnumerable<UserPortfolioItem>>> GetAllPortfolioItemsAsync(string portfolioId);
    Task<Result> CreateAsync(CreatePortfolioItem item);
    Task<Result> DeleteAsync(string portfolioItemId);
    Task<Result> DeleteAllPortfolioItemsAsync(string portfolioId);
}
