using Application.Core.Contract.Shared;
using Application.Core.Contract.UserInterface;
using Application.Core.Models.BlazorDTO.User;
using Application.Core.Models.Constants.Messages;
using CurrieTechnologies.Razor.SweetAlert2;
using FluentResults;
using Gian.Basic.Helper;
using Microsoft.AspNetCore.Components;
using Portfolio.tracker.Maui.Hybrid.Helpers;
using Portfolio.tracker.Maui.Hybrid.Models.Login;
using System.Globalization;

namespace Portfolio.tracker.Maui.Hybrid.Components.Pages.Login;

public partial class Login
{
    readonly LoginModel userData = new() { Username = string.Empty, Password = string.Empty };

    [Inject]
    public required IApplicationCulture ApplicationCulture { get; set; }

    [Inject]
    public required IUser UserService { get; set; }

    [Inject]
    public required IMessageHandling MessageHandling { get; set; }

    [Inject]
    public required IApplicationSession ApplicationSession { get; init; }

    [Inject]
    private NavigationManager Navigation { get; set; }

    private CultureInfo? culture;

    private string? Title;
    private string? UsernameLabel;
    private string? UsernamePlaceholder;
    private string? PasswordLabel;
    private string? LogInButtonText;
    private string? RememberMeLabel;
    private string? ForgotPasswordText;
    private string? DontHaveAnAccountText;
    private string? SignUpHereText;

    protected override void OnInitialized()
    {
        ApplicationSession.EndSession();

        culture = ApplicationCulture.GetCurrentCulture();

        Title = MessageHandling.GetMessage(Codes.UITextCodes.LogInCodes.Title, culture);
        UsernameLabel = MessageHandling.GetMessage(Codes.UITextCodes.LogInCodes.UsernameLabel, culture);
        UsernamePlaceholder = MessageHandling.GetMessage(Codes.UITextCodes.LogInCodes.UsernamePlaceholder, culture);
        PasswordLabel = MessageHandling.GetMessage(Codes.UITextCodes.LogInCodes.PasswordLabel, culture);
        RememberMeLabel = MessageHandling.GetMessage(Codes.UITextCodes.LogInCodes.RememberMeLabel, culture);
        LogInButtonText = MessageHandling.GetMessage(Codes.UITextCodes.LogInCodes.LogInButtonText, culture);
        ForgotPasswordText = MessageHandling.GetMessage(Codes.UITextCodes.LogInCodes.ForgotPasswordText, culture);
        DontHaveAnAccountText = MessageHandling.GetMessage(Codes.UITextCodes.LogInCodes.DontHaveAnAccountText, culture);
        SignUpHereText = MessageHandling.GetMessage(Codes.UITextCodes.LogInCodes.SignUpHereText, culture);
    }

    private async Task OnSubmit()
    {
        Result<LoggedInUserData> userResponse = await UserService.LogInAsync(userData.ToDto());

        if (userResponse.IsFailed)
        {
            await Swal.FireAsync(
                MessageHandling.GetMessage(Codes.TitleMessageCodes.ErrorTitle, culture),
                MessageHandling.GetMessage(userResponse.GetFirstError(), culture),
                SweetAlertIcon.Error);

            return;
        }

        ApplicationSession.CreateSession(userResponse.Value);

        Navigation.NavigateTo("/home", forceLoad: false);
    }
}