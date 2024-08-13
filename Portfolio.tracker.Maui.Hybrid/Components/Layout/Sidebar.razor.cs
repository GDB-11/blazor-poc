using Application.Core.Contract.PortfolioInterface;
using Application.Core.Contract.Shared;
using Application.Core.Models.BlazorDTO.User;
using Application.Core.Models.Constants.Messages;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Globalization;

namespace Portfolio.tracker.Maui.Hybrid.Components.Layout;

public partial class Sidebar
{
    [Inject]
    public required IApplicationCulture ApplicationCulture { get; set; }

    [Inject]
    public required IMessageHandling MessageHandling { get; set; }

    [Inject]
    public required IPortfolio Portfolio { get; init; }

    private CultureInfo? culture;

    private bool hasRendered = false;

    private readonly LoggedInUserData userData;

    private string? DashboardText;
    private string? PortfoliosText;
    private string? AssetText;
    private string? LogOutText;

    protected override void OnInitialized()
    {
        culture = ApplicationCulture.GetCurrentCulture();

        DashboardText = MessageHandling.GetMessage(Codes.UITextCodes.SidebarCodes.DashboardText, culture);
        PortfoliosText = MessageHandling.GetMessage(Codes.UITextCodes.SidebarCodes.PortfoliosText, culture);
        LogOutText = MessageHandling.GetMessage(Codes.UITextCodes.SidebarCodes.LogOutText, culture);
        AssetText = MessageHandling.GetMessage(Codes.UITextCodes.SidebarCodes.AssetText, culture);
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