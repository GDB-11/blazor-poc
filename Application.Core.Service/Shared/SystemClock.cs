using Application.Core.Contract.Shared;

namespace Application.Core.Service.Shared;

public sealed class SystemClock : IClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}
