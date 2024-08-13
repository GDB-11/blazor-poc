using Application.Core.Models.BlazorDTO.User;
using Microsoft.AspNetCore.Components;

namespace Portfolio.tracker.Maui.Hybrid.Components.Shared.NavigationBar;

public partial class NavigationBarUser
{
    [Parameter] public LoggedInUserData UserData { get; set; }
}