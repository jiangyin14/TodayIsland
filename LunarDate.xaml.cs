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
    "在主界面显示今日的农历日期。"
)]
public partial class LunarDateControl : ComponentBase
{
    public LunarDateControl()
    {
        InitializeComponent();
        LoadLunarDateAsync();
    }

    private string ConvertTraditionalDay(int day)
    {
        string[] nums = { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九" };

        if (day >= 1 && day <= 9)
            return "初" + nums[day];
        if (day == 10)
            return "初十";
        if (day > 10 && day < 20)
            return "十" + nums[day % 10];
        if (day == 20)
            return "二十";
        if (day > 20 && day < 30)
            return "廿" + nums[day % 20];
        if (day == 30)
            return "三十";

        return string.Empty;
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

            var monthText = ConvertToChinese(lunarMonth); // 保持原有中文月表示
            var dayText = ConvertTraditionalDay(lunarDay);
            var lunarDate = $"农历 {chineseEra}年{monthText}月{dayText}";

            Dispatcher.Invoke(() => LunarDate.Text = lunarDate);
        });
    }

    private string GetChineseEra(int year)
    {
        // 定义天干数组
        string[] heavenlyStems = { "甲", "乙", "丙", "丁", "戊", "己", "庚", "辛", "壬", "癸" };
        // 定义地支数组
        string[] earthlyBranches = { "子", "丑", "寅", "卯", "辰", "巳", "午", "未", "申", "酉", "戌", "亥" };

        // 计算天干索引
        int stemIndex = (year - 4) % 10;
        // 计算地支索引
        int branchIndex = (year - 4) % 12;

        // 返回天干地支组合
        return $"{heavenlyStems[stemIndex]}{earthlyBranches[branchIndex]}";
    }

    private string ConvertToChinese(int number)
    {
        // 定义一个数组，存储中文数字
        string[] chineseNumbers = { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九" };
        // 如果数字小于等于10，直接返回对应的中文数字
        if (number <= 10)
        {
            return chineseNumbers[number];
        }
        // 如果数字小于20，返回"十"加上个位数对应的中文数字
        else if (number < 20)
        {
            return "十" + (number % 10 == 0 ? "" : chineseNumbers[number % 10]);
        }
        // 如果数字大于等于20，返回十位数对应的中文数字加上"十"加上个位数对应的中文数字
        else
        {
            int tens = number / 10;
            int units = number % 10;
            return chineseNumbers[tens] + "十" + (units == 0 ? "" : chineseNumbers[units]);
        }
    }
}