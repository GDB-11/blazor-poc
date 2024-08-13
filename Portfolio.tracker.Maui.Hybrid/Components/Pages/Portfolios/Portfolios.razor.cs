using Application.Core.Contract.PortfolioInterface;
using Application.Core.Contract.Shared;
using Application.Core.Models.BlazorDTO.Portfolio;
using Application.Core.Models.Constants.Messages;
using CurrieTechnologies.Razor.SweetAlert2;
using FluentResults;
using Gian.Basic.Helper;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Portfolio.tracker.Maui.Hybrid.Helpers;
using Portfolio.tracker.Maui.Hybrid.Models.Portfolio;
using System.Globalization;

namespace Portfolio.tracker.Maui.Hybrid.Components.Pages.Portfolios;

public partial class Portfolios
{
    [Inject]
    public required IApplicationCulture ApplicationCulture { get; init; }

    [Inject]
    public required IApplicationSession ApplicationSession { get; init; }

    [Inject]
    public required IPortfolio Portfolio { get; init; }

    [Inject]
    public required IMessageHandling MessageHandling { get; init; }

    [Inject]
    public required SweetAlertService Swal { get; init; }

    [Inject]
    public required IJSRuntime JS { get; init; }

    [Inject]
    private NavigationManager Navigation { get; set; }

    private CultureInfo? culture;

    private List<UserPortfolio> userPortfolios = [];

    private readonly PortfolioModel portfolioModel = new() { Name = string.Empty };
    private readonly PortfolioModel portfolioEditModel = new() { Name = string.Empty };

    private bool hasRendered = false;

    private string? TitleText;
    private string? AddButtonText;
    private string? AddPortfolioModalTitleText;
    private string? NameTableHeaderText;
    private string? ActionTableHeaderText;
    private string? PortfolioNameModalFormText;
    private string? PortfolioNameModalFormPlaceholderText;
    private string? AddPortfolioModalButtonText;
    private string? EditPortfolioModalTitleText;
    private string? EditPortfolioModalButtonText;
    private string? ModalConfirmationTitleText;
    private string? PortfolioDeletionConfirmationBody;
    private string? PortfolioDeletionConfirmationYes;
    private string? PortfolioDeletionConfirmationNo;
    private string? PortfolioDeletionConfirmationMessage;
    private string? PortfolioDeletionCancellationMessage;

    private int SelectedRow;

    protected override async Task OnInitializedAsync()
    {
        culture = ApplicationCulture.GetCurrentCulture();

        if (ApplicationSession.HasSessionExpired())
        {
            Navigation.NavigateTo("/login", forceLoad: false);
        }

        ApplicationSession.RefreshSession();

        TitleText = MessageHandling.GetMessage(Codes.UITextCodes.SidebarCodes.PortfoliosText, culture);
        AddButtonText = MessageHandling.GetMessage(Codes.UITextCodes.CrudCodes.AddButtonText, culture);
        AddPortfolioModalTitleText = MessageHandling.GetMessage(Codes.UITextCodes.PortfolioCodes.AddPortfolioModalTitleText, culture);
        NameTableHeaderText = MessageHandling.GetMessage(Codes.UITextCodes.PortfolioCodes.NameTableHeaderText, culture);
        ActionTableHeaderText = MessageHandling.GetMessage(Codes.UITextCodes.PortfolioCodes.ActionTableHeaderText, culture);
        PortfolioNameModalFormText = MessageHandling.GetMessage(Codes.UITextCodes.PortfolioCodes.PortfolioNameModalFormText, culture);
        PortfolioNameModalFormPlaceholderText = MessageHandling.GetMessage(Codes.UITextCodes.PortfolioCodes.PortfolioNameModalFormPlaceholderText, culture);
        AddPortfolioModalButtonText = MessageHandling.GetMessage(Codes.UITextCodes.PortfolioCodes.AddPortfolioModalButtonText, culture);
        EditPortfolioModalTitleText = MessageHandling.GetMessage(Codes.UITextCodes.PortfolioCodes.EditPortfolioModalTitleText, culture);
        EditPortfolioModalButtonText = MessageHandling.GetMessage(Codes.UITextCodes.PortfolioCodes.EditPortfolioModalButtonText, culture);
        ModalConfirmationTitleText = MessageHandling.GetMessage(Codes.UITextCodes.ModalCodes.ConfirmationQuestion, culture);
        PortfolioDeletionConfirmationBody = MessageHandling.GetMessage(Codes.UITextCodes.PortfolioCodes.PortfolioDeletionConfirmationBody, culture);
        PortfolioDeletionConfirmationYes = MessageHandling.GetMessage(Codes.UITextCodes.ModalCodes.DeleteModalConfirmationYes, culture);
        PortfolioDeletionConfirmationNo = MessageHandling.GetMessage(Codes.UITextCodes.ModalCodes.DeleteModalConfirmationNo, culture);
        PortfolioDeletionConfirmationMessage = MessageHandling.GetMessage(Codes.UITextCodes.PortfolioCodes.PortfolioDeletionConfirmationMessage, culture);
        PortfolioDeletionCancellationMessage = MessageHandling.GetMessage(Codes.UITextCodes.PortfolioCodes.PortfolioDeletionCancellationMessage, culture);

        Result <IEnumerable<UserPortfolio>> portfoliosResult = await Portfolio.GetAllUserPortfoliosAsync(ApplicationSession.GetLoggedInUserData().UserId);

        if (ApplicationSession.HasSessionExpired())
        {
            await Swal.FireAsync(
                MessageHandling.GetMessage(Codes.TitleMessageCodes.ErrorTitle, culture),
                MessageHandling.GetMessage(portfoliosResult.GetFirstError(), culture),
                SweetAlertIcon.Error);

            return;
        }

        userPortfolios = portfoliosResult.Value.ToList();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && !hasRendered)
        {
            await JS.InvokeVoidAsync("initFlowbite");
            await JS.InvokeVoidAsync("getTableRowNumberFromButtonClicked");
            hasRendered = true;
        }
    }

    private async Task GetTableRowIdFromButton()
    {
        string value = await JS.InvokeAsync<string>("getTableRowNumberFromButtonClicked");

        SelectedRow = int.Parse(value);

        portfolioEditModel.Name = userPortfolios[SelectedRow].Name;
    }

    private async Task DeletePortfolio()
    {
        string value = await JS.InvokeAsync<string>("getTableRowNumberFromButtonClicked");

        SelectedRow = int.Parse(value);

        UserPortfolio selectedItem = userPortfolios[SelectedRow];

        SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
        {
            Title = ModalConfirmationTitleText,
            Text = string.Format(PortfolioDeletionConfirmationBody ?? string.Empty, selectedItem.Name),
            Icon = SweetAlertIcon.Warning,
            ShowCancelButton = true,
            ConfirmButtonText = PortfolioDeletionConfirmationYes,
            CancelButtonText = PortfolioDeletionConfirmationNo
        });

        if (!string.IsNullOrEmpty(result.Value))
        {
            Result deletionResult = await Portfolio.DeleteAsync(selectedItem.PortfolioId);

            if (deletionResult.IsFailed)
            {
                await Swal.FireAsync(
                    MessageHandling.GetMessage(Codes.TitleMessageCodes.ErrorTitle, culture),
                    MessageHandling.GetMessage(deletionResult.GetFirstError(), culture),
                    SweetAlertIcon.Error);

                return;
            }

            await Swal.FireAsync(
              MessageHandling.GetMessage(Codes.TitleMessageCodes.DeletedTitle, culture),
              string.Format(PortfolioDeletionConfirmationMessage ?? string.Empty, selectedItem.Name),
              SweetAlertIcon.Success);

            Navigation.NavigateTo(Navigation.Uri, forceLoad: true);
        }
        else if (result.Dismiss == DismissReason.Cancel)
        {
            await Swal.FireAsync(
              MessageHandling.GetMessage(Codes.TitleMessageCodes.CancelledTitle, culture),
              string.Format(PortfolioDeletionCancellationMessage ?? string.Empty, selectedItem.Name),
              SweetAlertIcon.Error);
        }
    }

    private async Task EnterPortfolio()
    {
        string value = await JS.InvokeAsync<string>("getTableRowNumberFromButtonClicked");

        SelectedRow = int.Parse(value);

        UserPortfolio selectedItem = userPortfolios[SelectedRow];

        Navigation.NavigateTo($"/Portfolio/{selectedItem.PortfolioId}", forceLoad: true);
    }

    private async Task OnAddSubmit()
    {
        portfolioModel.Name = await JS.InvokeAsync<string>("getElementValueById", "portfolio-add-name");
        Result portfolioResponse = await Portfolio.CreateAsync(portfolioModel.ToDto(ApplicationSession.GetLoggedInUserData().UserId));

        if (portfolioResponse.IsFailed)
        {
            await Swal.FireAsync(
                MessageHandling.GetMessage(Codes.TitleMessageCodes.ErrorTitle, culture),
                MessageHandling.GetMessage(portfolioResponse.GetFirstError(), culture),
                SweetAlertIcon.Error);

            return;
        }

        await Swal.FireAsync(
                MessageHandling.GetMessage(Codes.TitleMessageCodes.SuccessTitle, culture),
                MessageHandling.GetMessage(portfolioResponse.GetFirstSuccess(), culture),
                SweetAlertIcon.Success);

        Navigation.NavigateTo(Navigation.Uri, forceLoad: true);
    }

    private async Task OnEditSubmit()
    {
        UserPortfolio selectedItem = userPortfolios[SelectedRow];

        EditPortfolio portfolio = new()
        {
            PortfolioId = selectedItem.PortfolioId,
            Name = await JS.InvokeAsync<string>("getElementValueById", "portfolio-edit-name")
        };

        Result portfolioResponse = await Portfolio.EditAsync(portfolio);

        if (portfolioResponse.IsFailed)
        {
            await Swal.FireAsync(
                MessageHandling.GetMessage(Codes.TitleMessageCodes.ErrorTitle, culture),
                MessageHandling.GetMessage(portfolioResponse.GetFirstError(), culture),
                SweetAlertIcon.Error);

            return;
        }

        await Swal.FireAsync(
                MessageHandling.GetMessage(Codes.TitleMessageCodes.SuccessTitle, culture),
                MessageHandling.GetMessage(portfolioResponse.GetFirstSuccess(), culture),
                SweetAlertIcon.Success);

        Navigation.NavigateTo(Navigation.Uri, forceLoad: true);
    }
}