using Microsoft.AspNetCore.Components;

namespace Portfolio.tracker.Maui.Hybrid.Components.Shared.Table;

public partial class TableHeader
{
    [Parameter]
    public required string Text { get; set; }
}