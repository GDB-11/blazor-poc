using Microsoft.AspNetCore.Components;

namespace Portfolio.tracker.Maui.Hybrid.Components.Shared.Table.Cell;

public partial class BoldRowCell
{
    [Parameter]
    public required string Text { get; set; }
}