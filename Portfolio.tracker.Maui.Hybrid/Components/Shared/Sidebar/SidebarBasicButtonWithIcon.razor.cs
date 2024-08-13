using Microsoft.AspNetCore.Components;

namespace Portfolio.tracker.Maui.Hybrid.Components.Shared.Sidebar;

public partial class SidebarBasicButtonWithIcon
{
    [Parameter]
    public required RenderFragment ChildContent { get; set; }

    [Parameter]
    public required string Text { get; set; }

    [Parameter]
    public string? Route { get; set; }

    [Inject]
    private NavigationManager Navigation { get; set; }

    private void NavigateTo()
    {
        if (!string.IsNullOrEmpty(Route))
        {
            Navigation.NavigateTo($"/{Route}", forceLoad: false);
        }
    }
}