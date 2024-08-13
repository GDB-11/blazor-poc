using Microsoft.AspNetCore.Components;

namespace Portfolio.tracker.Maui.Hybrid.Components.Shared.Table.Cell;

public partial class BasicCell
{
    [Parameter]
    public required string Text { get; set; }
}