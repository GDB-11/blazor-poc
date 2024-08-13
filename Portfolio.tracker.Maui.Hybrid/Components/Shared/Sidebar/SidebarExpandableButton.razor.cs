using Microsoft.AspNetCore.Components;

namespace Portfolio.tracker.Maui.Hybrid.Components.Shared.Sidebar;

public partial class SidebarExpandableButton
{
    [Parameter]
    public RenderFragment ChildFragment { set => m_Children.Add(value); }

    [Parameter]
    public string Text { get; set; }

    private bool isDropdownVisible = false;
    private readonly List<RenderFragment> m_Children = [];

    private void ToggleDropdown()
    {
        isDropdownVisible = !isDropdownVisible;
    }
}