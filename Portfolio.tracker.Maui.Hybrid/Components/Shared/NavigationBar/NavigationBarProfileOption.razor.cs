using Microsoft.AspNetCore.Components;

namespace Portfolio.tracker.Maui.Hybrid.Components.Shared.NavigationBar;

public partial class NavigationBarProfileOption
{
    [Parameter] public required string Text { get; set; }
    [Parameter] public required string Route { get; set; }

    [Inject] private NavigationManager Navigation { get; set; }

    private void Navigate()
    {
        Navigation.NavigateTo($"/{Route}", forceLoad: false);
    }
}