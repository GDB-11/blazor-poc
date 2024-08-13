using Application.Core.Contract.Shared;
using Microsoft.AspNetCore.Components;

namespace Portfolio.tracker.Maui.Hybrid.Components.Pages.Home;

public partial class Home
{
    [Inject]
    public required IApplicationSession ApplicationSession { get; init; }

    [Inject]
    private NavigationManager Navigation { get; set; }

    protected override void OnInitialized()
    {
        if (ApplicationSession.HasSessionExpired())
        {
            Navigation.NavigateTo("/login", forceLoad: false);
        }
    }
}