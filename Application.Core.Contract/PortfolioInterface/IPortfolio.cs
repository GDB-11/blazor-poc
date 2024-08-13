using Application.Core.Models.BlazorDTO.Portfolio;
using FluentResults;

namespace Application.Core.Contract.PortfolioInterface;

public interface IPortfolio
{
    Task<Result<IEnumerable<UserPortfolio>>> GetAllUserPortfoliosAsync(string userId);
    Task<Result<UserPortfolio>> GetUserPortfolioAsync(string portfolioId, string userId);
    Task<Result> CreateAsync(CreatePortfolio portfolio);
    Task<Result> EditAsync(EditPortfolio portfolio);
    Task<Result> DeleteAsync(string portfolioId);
}
