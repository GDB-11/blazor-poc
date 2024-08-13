using Microsoft.AspNetCore.Components;

namespace Portfolio.tracker.Maui.Hybrid.Components.Shared.Table.Cell;

public partial class BasicCellWithIcon
{
    [Parameter]
    public required RenderFragment ChildContent { get; set; }
}