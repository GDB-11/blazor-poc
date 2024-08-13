using Application.Core.Contract.Shared;
using Application.Core.Models.Constants.Application;
using Microsoft.AspNetCore.Components;

namespace Portfolio.tracker.Maui.Hybrid.Components.Shared.Dropdown;

public partial class DropdownHeaderWithIcon
{
    [Inject]
    public required IApplicationCulture ApplicationCulture { get; set; }

    public string? Culture { get; set; }

    public required string Text { get; set; }

    protected override void OnInitialized()
    {
        Culture = ApplicationCulture.GetCultureName();

        Text = Culture switch
        {
            Languages.English => Languages.EnglishText,
            Languages.Spanish => Languages.SpanishText,
            _ => Languages.EnglishText,
        };
    }
}