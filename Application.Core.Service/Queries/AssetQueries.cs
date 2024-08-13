namespace Application.Core.Service.Queries;

internal static class AssetQueries
{
    internal const string GetAssetIdByTicker =
        @"SELECT
			AssetId
			, Name
			, Ticker
			, Logo
			, CreatedDate
		FROM
			Asset
		WHERE
			Ticker = @Ticker";
}
