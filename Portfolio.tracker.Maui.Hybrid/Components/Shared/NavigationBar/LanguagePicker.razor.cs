using Application.Core.Contract.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Portfolio.tracker.Maui.Hybrid.Components.Shared.NavigationBar;

public partial class LanguagePicker
{
    [Inject]
    public required IApplicationCulture ApplicationCulture { get; set; }

    [Inject]
    public required IJSRuntime JSRuntime { get; set; }

    [Inject]
    public required NavigationManager Navigation { get; set; }

    async Task ChangeCulture(string selectedCultureValue)
    {
        ApplicationCulture.SetCurrentCulture(selectedCultureValue);
        await JSRuntime.InvokeVoidAsync("hideLanguageDropdown", "language-dropdown");
        Navigation.NavigateTo(Navigation.Uri, forceLoad: true);
    }
}