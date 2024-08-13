using Application.Core.Models.BlazorDTO.User;
using Microsoft.JSInterop;

namespace Portfolio.tracker.Maui.Hybrid.Components.Layout;

public partial class NavMenu
{
    private bool hasRendered = false;

    private LoggedInUserData userData;

    protected override void OnInitialized()
    {

    }

    //protected override async Task OnInitializedAsync()
    //{
    //    //await ...
    //}

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && !hasRendered)
        {
            await JS.InvokeVoidAsync("initFlowbite");
            hasRendered = true;
        }
    }
}