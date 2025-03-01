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

            var lunarDate = $"农历 {chineseEra}年{ConvertToChinese(lunarMonth)}月{ConvertToChinese(lunarDay)}日";
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

    private string ConvertToChinese(int number)
    {
        string[] chineseNumbers = { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九" };
        if (number <= 10)
        {
            return chineseNumbers[number];
        }
        else if (number < 20)
        {
            return "十" + (number % 10 == 0 ? "" : chineseNumbers[number % 10]);
        }
        else
        {
            int tens = number / 10;
            int units = number % 10;
            return chineseNumbers[tens] + "十" + (units == 0 ? "" : chineseNumbers[units]);
        }
    }
}