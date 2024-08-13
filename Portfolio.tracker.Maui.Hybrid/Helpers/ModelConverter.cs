using Application.Core.Models.BlazorDTO.Portfolio;
using Application.Core.Models.BlazorDTO.PortfolioItem;
using Application.Core.Models.BlazorDTO.User;
using Portfolio.tracker.Maui.Hybrid.Models.Login;
using Portfolio.tracker.Maui.Hybrid.Models.Portfolio;
using Portfolio.tracker.Maui.Hybrid.Models.Register;

namespace Portfolio.tracker.Maui.Hybrid.Helpers;

internal static class ModelConverter
{
    internal static RegisteredUserData ToDto(this SignupModel model)
    {
        return new RegisteredUserData
        {
            Username = model.Username,
            Password = model.Password,
            Email = model.Email
        };
    }

    internal static LogInUserData ToDto(this LoginModel model)
    {
        return new LogInUserData
        {
            Username = model.Username,
            Password = model.Password
        };
    }

    internal static CreatePortfolio ToDto(this PortfolioModel model, string userId)
    {
        return new CreatePortfolio
        {
            UserId = userId,
            Name = model.Name
        };
    }

    internal static CreatePortfolioItem ToDto(this PortfolioItemModel model, string userId, string portfolioId)
    {
        return new CreatePortfolioItem
        {
            Name = model.Name,
            Ticker = model.Ticker,
            PortfolioId = portfolioId
            //Logo = model.Logo
        };
    }
}
