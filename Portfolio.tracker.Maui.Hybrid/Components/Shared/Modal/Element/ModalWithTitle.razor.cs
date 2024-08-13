using Microsoft.AspNetCore.Components;

namespace Portfolio.tracker.Maui.Hybrid.Components.Shared.Modal.Element;

public partial class ModalWithTitle
{
    [Parameter]
    public required string ModalId { get; set; }

    [Parameter]
    public required string TitleText { get; set; }

    [Parameter]
    public required RenderFragment ChildContent { get; set; }
}