using ClassIsland.Core.Abstractions.Controls;
using ClassIsland.Core.Attributes;
using MaterialDesignThemes.Wpf;

namespace TodayIsland;

[ComponentInfo(
    "4AD7D5E4-E999-7FD2-F3D7-61D5EA400920",
    "农历日期",
    PackIconKind.CalendarOutline,
    "在主界面显示本周在学期中的周数。"
)]
public partial class LunarDateControl : ComponentBase
{
    public LunarDateControl()
    {
        InitializeComponent();
        LoadLunarDateAsync();
    }

    private async void LoadLunarDateAsync()
    {
    }
}