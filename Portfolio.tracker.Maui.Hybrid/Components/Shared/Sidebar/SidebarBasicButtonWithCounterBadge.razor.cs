using Microsoft.AspNetCore.Components;

namespace Portfolio.tracker.Maui.Hybrid.Components.Shared.Sidebar;

public partial class SidebarBasicButtonWithCounterBadge
{
    [Parameter] public required RenderFragment ChildContent { get; set; }
    [Parameter] public required string Text { get; set; }
    [Parameter] public required string BadgeNumber { get; set; }
}