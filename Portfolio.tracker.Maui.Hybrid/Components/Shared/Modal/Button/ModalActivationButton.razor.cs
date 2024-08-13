using Microsoft.AspNetCore.Components;

namespace Portfolio.tracker.Maui.Hybrid.Components.Shared.Modal.Button;

public partial class ModalActivationButton
{
    [Parameter]
    public required string TargetModalId { get; set; }

    [Parameter]
    public required string Text { get; set; }
}