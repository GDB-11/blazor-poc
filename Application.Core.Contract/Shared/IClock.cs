namespace Application.Core.Contract.Shared;

public interface IClock
{
    DateTime UtcNow { get; }
}
