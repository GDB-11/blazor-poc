using Microsoft.AspNetCore.Components;

namespace Portfolio.tracker.Maui.Hybrid.Components.Shared.FormElement;

public partial class BasicLabeledInput
{
    [Parameter]
    public required string Id { get; set; }

    [Parameter]
    public required string Label { get; set; }

    [Parameter]
    public string? PlaceholderText { get; set; }

    [Parameter]
    public string Type { get; set; } = "text";

    [Parameter]
    public bool IsRequired { get; set; } = false;

    [Parameter]
    public string? Value { get; set; }

    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }
}