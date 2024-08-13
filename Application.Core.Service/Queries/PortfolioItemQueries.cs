namespace Application.Core.Service.Queries;

internal static class PortfolioItemQueries
{
    internal const string GetPortfolioItemsByPortfolioId =
        @"SELECT
			pi.PortfolioItemId
			, pi.AssetId
			, ast.Name
			, ast.Ticker
			, ast.Logo
			, pi.Display
		FROM
			PortfolioItem pi
			INNER JOIN Asset ast ON pi.AssetId = ast.AssetId
		WHERE
			pi.PortfolioId = @PortfolioId";

	internal const string DeletePortfolioItemByPortfolioId =
        @"DELETE FROM
			PortfolioItem
		WHERE
			PortfolioItemId = @PortfolioItemId";

    internal const string DeletePortfolioItemsByPortfolioId =
        @"DELETE FROM
			PortfolioItem
		WHERE
			PortfolioId = @PortfolioId";
}
