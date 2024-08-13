using Application.Core.Contract.Shared;

namespace Application.Core.Service.Shared;

public sealed class TimeBasedOneTimePasswordService : ITimeBasedOneTimePassword
{
    private readonly ITimeBasedOneTimePassword _timeBasedOneTimePassword;

    public TimeBasedOneTimePasswordService(ITimeBasedOneTimePassword timeBasedOneTimePassword)
    {
        _timeBasedOneTimePassword = timeBasedOneTimePassword;
    }


}
