using Application.Core.Models.BlazorDTO.User;

namespace Application.Core.Contract.Shared;

public interface IApplicationSession
{
    void CreateSession(LoggedInUserData user);
    void RefreshSession();
    void EndSession();
    LoggedInUserData GetLoggedInUserData();
    bool HasSessionExpired();
}
