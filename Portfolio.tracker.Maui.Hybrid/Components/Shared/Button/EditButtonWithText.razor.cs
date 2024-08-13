using Microsoft.AspNetCore.Components;

namespace Portfolio.tracker.Maui.Hybrid.Components.Shared.Button;

public partial class EditButtonWithText
{
    [Parameter]
    public required string Id { get; set; }

    [Parameter]
    public required string Text { get; set; }

    [Parameter]
    public string Type { get; set; } = "text";
}