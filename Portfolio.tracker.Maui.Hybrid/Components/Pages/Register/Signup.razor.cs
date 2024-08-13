using Application.Core.Contract.Shared;
using Application.Core.Contract.UserInterface;
using Application.Core.Models.Constants.Messages;
using CurrieTechnologies.Razor.SweetAlert2;
using FluentResults;
using Gian.Basic.Helper;
using Microsoft.AspNetCore.Components;
using Portfolio.tracker.Maui.Hybrid.Helpers;
using Portfolio.tracker.Maui.Hybrid.Models.Register;
using System.Globalization;

namespace Portfolio.tracker.Maui.Hybrid.Components.Pages.Register;

public partial class Signup
{
    private readonly SignupModel userData = new() { Username = string.Empty, Password = string.Empty, Email = string.Empty };

    [Inject]
    public required IApplicationCulture ApplicationCulture { get; set; }

    [Inject]
    public required IUser UserService { get; set; }

    [Inject]
    public required IMessageHandling MessageHandling { get; set; }

    [Inject]
    private NavigationManager Navigation { get; set; }

    private CultureInfo? culture;

    private string? Title;
    private string? UsernameLabel;
    private string? UsernamePlaceholder;
    private string? PasswordLabel;
    private string? EmailLabel;
    private string? EmailPlaceholder;
    private string? TermsAndConditionsLabel;
    private string? CreateAccountButtonText;
    private string? AlreadyHaveAnAccountText;
    private string? LoginHereText;

    protected override void OnInitialized()
    {
        culture = ApplicationCulture.GetCurrentCulture();

        Title = MessageHandling.GetMessage(Codes.UITextCodes.RegisterCodes.Title, culture);
        UsernameLabel = MessageHandling.GetMessage(Codes.UITextCodes.RegisterCodes.UsernameLabel, culture);
        UsernamePlaceholder = MessageHandling.GetMessage(Codes.UITextCodes.RegisterCodes.UsernamePlaceholder, culture);
        PasswordLabel = MessageHandling.GetMessage(Codes.UITextCodes.RegisterCodes.PasswordLabel, culture);
        EmailLabel = MessageHandling.GetMessage(Codes.UITextCodes.RegisterCodes.EmailLabel, culture);
        EmailPlaceholder = MessageHandling.GetMessage(Codes.UITextCodes.RegisterCodes.EmailPlaceholder, culture);
        TermsAndConditionsLabel = MessageHandling.GetMessage(Codes.UITextCodes.RegisterCodes.TermsAndConditionsLabel, culture);
        CreateAccountButtonText = MessageHandling.GetMessage(Codes.UITextCodes.RegisterCodes.CreateAccountButtonText, culture);
        AlreadyHaveAnAccountText = MessageHandling.GetMessage(Codes.UITextCodes.RegisterCodes.AlreadyHaveAnAccountText, culture);
        LoginHereText = MessageHandling.GetMessage(Codes.UITextCodes.RegisterCodes.LoginHereText, culture);
    }

    private async Task OnSubmit()
    {
        if (!userData.AcceptTC)
        {
            await Swal.FireAsync(
                MessageHandling.GetMessage(Codes.TitleMessageCodes.WarningTitle, culture),
                MessageHandling.GetMessage(Codes.UserCodes.UserAcceptTermsAndConditionsError, culture),
                SweetAlertIcon.Warning);

            return;
        }

        bool usernameValidation = await IsValidUsername();

        if (!usernameValidation)
        {
            return;
        }

        bool emailValidation = await IsValidEmail();

        if (!emailValidation)
        {
            return;
        }

        bool passwordValidation = await IsValidPassword();

        if (!passwordValidation)
        {
            return;
        }

        Result userResponse = await UserService.CreateAsync(userData.ToDto());

        if (userResponse.IsFailed)
        {
            await Swal.FireAsync(
                MessageHandling.GetMessage(Codes.TitleMessageCodes.ErrorTitle, culture),
                MessageHandling.GetMessage(userResponse.GetFirstError(), culture),
                SweetAlertIcon.Error);

            return;
        }

        await Swal.FireAsync(
                MessageHandling.GetMessage(Codes.TitleMessageCodes.SuccessTitle, culture),
                MessageHandling.GetMessage(userResponse.GetFirstSuccess(), culture),
                SweetAlertIcon.Success);

        Navigation.NavigateTo("/login", forceLoad: false);
    }

    private async Task<bool> IsValidUsername()
    {
        Result response = await UserService.DoesUsernameExistAsync(userData.Username);

        if (response.IsFailed)
        {
            await Swal.FireAsync(
                MessageHandling.GetMessage(Codes.TitleMessageCodes.ErrorTitle, culture),
                MessageHandling.GetMessage(response.GetFirstError(), culture),
                SweetAlertIcon.Error);

            return false;
        }

        return true;
    }

    private async Task<bool> IsValidEmail()
    {
        Result response = await UserService.DoesEmailExistAsync(userData.Email);

        if (response.IsFailed)
        {
            await Swal.FireAsync(
                MessageHandling.GetMessage(Codes.TitleMessageCodes.ErrorTitle, culture),
                MessageHandling.GetMessage(response.GetFirstError(), culture),
                SweetAlertIcon.Error);

            return false;
        }

        return true;
    }

    private async Task<bool> IsValidPassword()
    {
        if (string.IsNullOrEmpty(userData.Password))
        {
            await Swal.FireAsync(
                MessageHandling.GetMessage(Codes.TitleMessageCodes.ErrorTitle, culture),
                MessageHandling.GetMessage(Codes.UserCodes.UserPasswordEmptyError, culture),
                SweetAlertIcon.Error);

            return false;
        }

        return true;
    }
}