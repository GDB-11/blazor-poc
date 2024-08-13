namespace Application.Core.Service.Queries;

internal static class PortfolioQueries
{
    internal const string GetUserPortfolios =
        @"SELECT
			PortfolioId
			, UserId
			, Name
			, Status
		FROM
			Portfolio
		WHERE
			UserId = @UserId";

    internal const string GetUserPortfolio =
        @"SELECT
			PortfolioId
			, UserId
			, Name
			, Status
		FROM
			Portfolio
		WHERE
			PortfolioId = @PortfolioId
			AND UserId = @UserId";

    internal const string UpdateUserPortfolio =
        @"UPDATE
			Portfolio
		SET
			Name = @Name
			, UpdatedDate = @UpdatedDate
		WHERE
			PortfolioId = @PortfolioId";

    internal const string DeleteUserPortfolio =
        @"DELETE FROM
			Portfolio
		WHERE
			PortfolioId = @PortfolioId";
}
