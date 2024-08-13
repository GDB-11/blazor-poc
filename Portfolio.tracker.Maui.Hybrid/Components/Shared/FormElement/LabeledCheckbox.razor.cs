using Microsoft.AspNetCore.Components;

namespace Portfolio.tracker.Maui.Hybrid.Components.Shared.FormElement;

public partial class LabeledCheckbox
{
    [Parameter]
    public required string Id { get; set; }

    [Parameter]
    public required string Label { get; set; }

    [Parameter]
    public bool IsRequired { get; set; } = false;

    [Parameter]
    public bool Value { get; set; }

    [Parameter]
    public EventCallback<bool> ValueChanged { get; set; }

    private async Task OnValueChanged(ChangeEventArgs e)
    {
        Value = (bool)e.Value;

        await ValueChanged.InvokeAsync(Value);
    }
}