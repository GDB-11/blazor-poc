using Application.Core.Contract.Shared;
using Application.Core.Models.Constants.Messages;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Globalization;

namespace Portfolio.tracker.Maui.Hybrid.Components.Layout;

public partial class LoginLayout
{
    [Inject]
    public required IApplicationCulture ApplicationCulture { get; set; }

    [Inject]
    public required IMessageHandling MessageHandling { get; set; }

    private bool hasRendered = false;

    private string Motto = string.Empty;

    protected override void OnInitialized()
    {
        CultureInfo culture = ApplicationCulture.GetCurrentCulture();

        Motto = MessageHandling.GetMessage(Codes.UITextCodes.Motto, culture);
        //await ...
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && !hasRendered)
        {
            await JS.InvokeVoidAsync("initFlowbite");
            hasRendered = true;
        }
    }
}