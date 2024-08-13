using Application.Core.Contract.PortfolioInterface;
using Application.Core.Contract.Shared;
using Application.Core.Contract.UserInterface;
using Application.Core.Models.BlazorDTO.Portfolio;
using Application.Core.Models.BlazorDTO.PortfolioItem;
using Application.Core.Models.BlazorDTO.User;
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

public partial class PortfolioView
{
    [Parameter]
    public required string PortfolioId { get; set; }

    [Inject]
    public required IApplicationCulture ApplicationCulture { get; set; }

    [Inject]
    public required IUser UserService { get; set; }

    [Inject]
    public required IPortfolio Portfolio { get; init; }

    [Inject]
    public required IPortfolioItem PortfolioItem { get; init; }

    [Inject]
    public required IMessageHandling MessageHandling { get; set; }

    [Inject]
    public required IApplicationSession ApplicationSession { get; init; }

    [Inject]
    public required SweetAlertService Swal { get; init; }

    [Inject]
    public required IJSRuntime JS { get; init; }

    [Inject]
    private NavigationManager Navigation { get; set; }

    private LoggedInUserData user;
    private UserPortfolio portfolio;
    private List<UserPortfolioItem> userPortfolioItems = [];

    private CultureInfo? culture;

    private bool hasRendered = false;

    private readonly PortfolioItemModel portfolioItemModel = new() { Name = string.Empty, PortfolioId = string.Empty, Ticker = string.Empty, UserId = string.Empty };

    private string? TitleText;
    private string? AddButtonText;
    private string? AddPortfolioItemModalTitleText;
    private string? AddPortfolioItemModalButtonText;
    private string? PortfolioItemNameModalFormText;
    private string? PortfolioItemNameModalFormPlaceholderText;
    private string? PortfolioItemTickerModalFormText;
    private string? PortfolioItemTickerModalFormPlaceholderText;
    private string? NameTableHeaderText;
    private string? TickerTableHeaderText;
    private string? LogoTableHeaderText;
    private string? ActionTableHeaderText;
    private string? DeletePortfolioItemModalConfirmationTitleText;
    private string? DeletePortfolioItemConfirmationBody;
    private string? DeleteModalConfirmationYes;
    private string? DeleteModalConfirmationNo;
    private string? PortfolioItemDeletionConfirmationMessage;
    private string? PortfolioItemDeletionCancellationMessage;

    private int SelectedRow;

    protected override async Task OnInitializedAsync()
    {
        culture = ApplicationCulture.GetCurrentCulture();

        if (ApplicationSession.HasSessionExpired())
        {
            Navigation.NavigateTo("/login", forceLoad: false);
        }

        ApplicationSession.RefreshSession();

        user = ApplicationSession.GetLoggedInUserData();

        Result<UserPortfolio> portfolioResult = await Portfolio.GetUserPortfolioAsync(PortfolioId, user.UserId);

        if (portfolioResult.IsFailed)
        {
            await Swal.FireAsync(
                    MessageHandling.GetMessage(Codes.TitleMessageCodes.ErrorTitle, culture),
                    MessageHandling.GetMessage(portfolioResult.GetFirstError(), culture),
                    SweetAlertIcon.Error);

            return;
        }

        if (string.IsNullOrEmpty(portfolioResult.Value.PortfolioId))
        {
            Navigation.NavigateTo($"/portfolios", forceLoad: true);
        }

        portfolio = portfolioResult.Value;

        TitleText = portfolio.Name;
        AddButtonText = MessageHandling.GetMessage(Codes.UITextCodes.PortfolioItemCodes.PortfolioItemAddButtonText, culture);
        AddPortfolioItemModalTitleText = MessageHandling.GetMessage(Codes.UITextCodes.PortfolioItemCodes.AddPortfolioItemModalTitleText, culture);
        AddPortfolioItemModalButtonText = MessageHandling.GetMessage(Codes.UITextCodes.PortfolioItemCodes.AddPortfolioItemModalButtonText, culture);
        PortfolioItemNameModalFormText = MessageHandling.GetMessage(Codes.UITextCodes.PortfolioItemCodes.PortfolioItemNameModalFormText, culture);
        PortfolioItemTickerModalFormText = MessageHandling.GetMessage(Codes.UITextCodes.PortfolioItemCodes.PortfolioItemTickerModalFormText, culture);
        PortfolioItemTickerModalFormPlaceholderText = MessageHandling.GetMessage(Codes.UITextCodes.PortfolioItemCodes.PortfolioItemTickerModalFormPlaceholderText, culture);
        PortfolioItemNameModalFormPlaceholderText = MessageHandling.GetMessage(Codes.UITextCodes.PortfolioItemCodes.PortfolioItemNameModalFormPlaceholderText, culture);
        NameTableHeaderText = MessageHandling.GetMessage(Codes.UITextCodes.PortfolioItemCodes.PortfolioItemNameTableHeaderText, culture);
        TickerTableHeaderText = MessageHandling.GetMessage(Codes.UITextCodes.PortfolioItemCodes.PortfolioItemTickerTableHeaderText, culture);
        LogoTableHeaderText = MessageHandling.GetMessage(Codes.UITextCodes.PortfolioItemCodes.PortfolioItemLogoTableHeaderText, culture);
        ActionTableHeaderText = MessageHandling.GetMessage(Codes.UITextCodes.PortfolioItemCodes.PortfolioItemActionTableHeaderText, culture);
        DeletePortfolioItemModalConfirmationTitleText = MessageHandling.GetMessage(Codes.UITextCodes.ModalCodes.ConfirmationQuestion, culture);
        DeletePortfolioItemConfirmationBody = MessageHandling.GetMessage(Codes.UITextCodes.PortfolioItemCodes.DeletePortfolioItemConfirmationBody, culture);
        DeleteModalConfirmationYes = MessageHandling.GetMessage(Codes.UITextCodes.ModalCodes.DeleteModalConfirmationYes, culture);
        DeleteModalConfirmationNo = MessageHandling.GetMessage(Codes.UITextCodes.ModalCodes.DeleteModalConfirmationNo, culture);
        PortfolioItemDeletionConfirmationMessage = MessageHandling.GetMessage(Codes.UITextCodes.PortfolioItemCodes.PortfolioItemDeletionConfirmationMessage, culture);
        PortfolioItemDeletionCancellationMessage = MessageHandling.GetMessage(Codes.UITextCodes.PortfolioItemCodes.PortfolioItemDeletionCancellationMessage, culture);

        Result<IEnumerable<UserPortfolioItem>> userPortfolioItemsResult = await PortfolioItem.GetAllPortfolioItemsAsync(portfolio.PortfolioId);

        if (userPortfolioItemsResult.IsFailed)
        {
            await Swal.FireAsync(
                    MessageHandling.GetMessage(Codes.TitleMessageCodes.ErrorTitle, culture),
                    MessageHandling.GetMessage(userPortfolioItemsResult.GetFirstError(), culture),
                    SweetAlertIcon.Error);

            return;
        }

        userPortfolioItems = userPortfolioItemsResult.Value.ToList();
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

    private async Task OnAddSubmit()
    {
        portfolioItemModel.Name = await JS.InvokeAsync<string>("getElementValueById", "portfolio-item-add-name");
        portfolioItemModel.Ticker = await JS.InvokeAsync<string>("getElementValueById", "portfolio-item-add-ticker");

        Result portfolioItemResponse = await PortfolioItem.CreateAsync(portfolioItemModel.ToDto(user.UserId, PortfolioId));

        if (portfolioItemResponse.IsFailed)
        {
            await Swal.FireAsync(
                MessageHandling.GetMessage(Codes.TitleMessageCodes.ErrorTitle, culture),
                MessageHandling.GetMessage(portfolioItemResponse.GetFirstError(), culture),
                SweetAlertIcon.Error);

            return;
        }

        await Swal.FireAsync(
                MessageHandling.GetMessage(Codes.TitleMessageCodes.SuccessTitle, culture),
                MessageHandling.GetMessage(portfolioItemResponse.GetFirstSuccess(), culture),
                SweetAlertIcon.Success);

        Navigation.NavigateTo(Navigation.Uri, forceLoad: true);
    }

    private async Task ViewPortfolioItem()
    {
        string value = await JS.InvokeAsync<string>("getTableRowNumberFromButtonClicked");

        SelectedRow = int.Parse(value);

        UserPortfolioItem selectedItem = userPortfolioItems[SelectedRow];

        //Navigation.NavigateTo($"/ItemPortfolio/{selectedItem.PortfolioItemId}", forceLoad: true);
    }

    private async Task AddPortfolioItemTransaction()
    {
        string value = await JS.InvokeAsync<string>("getTableRowNumberFromButtonClicked");

        SelectedRow = int.Parse(value);

        UserPortfolioItem selectedItem = userPortfolioItems[SelectedRow];
    }

    private async Task DeletePortfolioItem()
    {
        string value = await JS.InvokeAsync<string>("getTableRowNumberFromButtonClicked");

        SelectedRow = int.Parse(value);

        UserPortfolioItem selectedItem = userPortfolioItems[SelectedRow];

        SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
        {
            Title = DeletePortfolioItemModalConfirmationTitleText,
            Text = string.Format(DeletePortfolioItemConfirmationBody ?? string.Empty, selectedItem.Name),
            Icon = SweetAlertIcon.Warning,
            ShowCancelButton = true,
            ConfirmButtonText = DeleteModalConfirmationYes,
            CancelButtonText = DeleteModalConfirmationNo
        });

        if (!string.IsNullOrEmpty(result.Value))
        {
            Result deletionResult = await PortfolioItem.DeleteAsync(selectedItem.PortfolioItemId);

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
              string.Format(PortfolioItemDeletionConfirmationMessage ?? string.Empty, selectedItem.Name),
              SweetAlertIcon.Success);

            Navigation.NavigateTo(Navigation.Uri, forceLoad: true);
        }
        else if (result.Dismiss == DismissReason.Cancel)
        {
            await Swal.FireAsync(
              MessageHandling.GetMessage(Codes.TitleMessageCodes.CancelledTitle, culture),
              string.Format(PortfolioItemDeletionCancellationMessage ?? string.Empty, selectedItem.Name),
              SweetAlertIcon.Error);
        }
    }
}