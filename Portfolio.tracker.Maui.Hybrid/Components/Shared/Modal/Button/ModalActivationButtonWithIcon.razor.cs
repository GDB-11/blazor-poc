using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Portfolio.tracker.Maui.Hybrid.Components.Shared.Modal.Button;

public partial class ModalActivationButtonWithIcon
{
    [Parameter]
    public required string TargetModalId { get; set; }

    [Parameter]
    public required RenderFragment ChildContent { get; set; }

    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    private async Task HandleClick(MouseEventArgs e)
    {
        if (OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync(e);
        }
    }
}