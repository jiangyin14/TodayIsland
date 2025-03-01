using System;
using System.Globalization;
using System.Threading.Tasks;
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
        await Task.Run(() =>
        {
            var chineseCalendar = new ChineseLunisolarCalendar();
            var today = DateTime.Now;
            var lunarYear = chineseCalendar.GetYear(today);
            var lunarMonth = chineseCalendar.GetMonth(today);
            var lunarDay = chineseCalendar.GetDayOfMonth(today);
            var chineseEra = GetChineseEra(lunarYear);

            var lunarDate = $"农历 {chineseEra}年{lunarMonth}月{lunarDay}日";
            Dispatcher.Invoke(() => LunarDate.Text = lunarDate);
        });
    }

    private string GetChineseEra(int year)
    {
        string[] heavenlyStems = { "甲", "乙", "丙", "丁", "戊", "己", "庚", "辛", "壬", "癸" };
        string[] earthlyBranches = { "子", "丑", "寅", "卯", "辰", "巳", "午", "未", "申", "酉", "戌", "亥" };

        int stemIndex = (year - 4) % 10;
        int branchIndex = (year - 4) % 12;

        return $"{heavenlyStems[stemIndex]}{earthlyBranches[branchIndex]}";
    }
}