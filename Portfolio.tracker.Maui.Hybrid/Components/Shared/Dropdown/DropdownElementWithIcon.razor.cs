using Microsoft.AspNetCore.Components;

namespace Portfolio.tracker.Maui.Hybrid.Components.Shared.Dropdown;

public partial class DropdownElementWithIcon
{
    [Parameter]
    public required RenderFragment ChildContent { get; set; }

    [Parameter]
    public string? Text { get; set; }

    [Parameter]
    public required Action OnClickFunction { get; set; }
}