namespace Application.Core.Models.Application.Config;

public sealed class ConnectionStrings
{
    public required string AppSqliteConnectionString { get; init; }
    public required string TxLiteDbConnectionString { get; init; }
}
