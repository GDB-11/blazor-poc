namespace Application.Core.Service.Queries;

internal static class TransactionQueries
{
    internal const string UpdateTransactionByTransactionId =
        @"UPDATE
			transactions
		SET
			OperationType = @OperationType
			, Price = @Price
			, Quantity = @Quantity
			, TransactionDateTime = @TransactionDateTime
			, TransactionHash = @TransactionHash
			, TransactionUrl = @TransactionUrl
			, UpdateDate = @UpdateDate
		WHERE
			TransactionId = @TransactionId";

    internal const string DeleteTransactionByTransactionId =
        "DELETE transactions WHERE TransactionId = @TransactionId";

    internal const string DeleteTransactionsByPortfolioItemId =
        "DELETE transactions WHERE PortfolioItemId = @PortfolioItemId";

    internal const string DeleteTransactionsByPortfolioId =
        "DELETE transactions WHERE PortfolioId = @PortfolioId";
}
