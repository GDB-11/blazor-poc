using Microsoft.AspNetCore.Components;

namespace Portfolio.tracker.Maui.Hybrid.Components.Shared.Table;

public partial class HoverableTableRow
{
    [Parameter]
    public RenderFragment ChildFragment { set => m_Children.Add(value); }

    private readonly List<RenderFragment> m_Children = [];
}