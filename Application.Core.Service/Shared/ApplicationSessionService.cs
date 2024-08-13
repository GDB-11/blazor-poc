using Application.Core.Contract.Shared;
using Application.Core.Models.Application.Shared;
using Application.Core.Models.BlazorDTO.User;

namespace Application.Core.Service.Shared;

public sealed class ApplicationSessionService : IApplicationSession
{
    private readonly IClock _clock;

    private AppSession? _session;

    public ApplicationSessionService(IClock clock)
    {
        _clock = clock;
    }

    public void CreateSession(LoggedInUserData user)
    {
        _session = new AppSession { User = user, ExpirationDate = _clock.UtcNow.AddMinutes(30D) };
    }

    public void RefreshSession()
    {
        _session.ExpirationDate = _clock.UtcNow.AddHours(30D);
    }

    public void EndSession()
    {
        _session = null;
    }

    public LoggedInUserData GetLoggedInUserData()
    {
        return _session.User;
    }

    public bool HasSessionExpired()
    {
        if (_session is null) 
        {
            return true;
        }

        return _clock.UtcNow > _session.ExpirationDate;
    }
}
