using ClassIsland.Core.Abstractions.Controls;
using ClassIsland.Core.Attributes;
using MaterialDesignThemes.Wpf;

namespace TodayIsland;

[ComponentInfo(
    "6DD56598-8C6E-C22D-D642-053EC1B0A1F1",
    "日月起落时间",
    PackIconKind.CalendarOutline,
    "在主界面显示本周在学期中的周数。"
)]
public partial class RiseFallTimeControl : ComponentBase
{
    public RiseFallTimeControl()
    {
        InitializeComponent();
        LoadRiseFallTimeAsync();
    }

    private async void LoadRiseFallTimeAsync()
    {
        
    }
}