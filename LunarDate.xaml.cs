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
        // 异步加载农历日期
        await Task.Run(() =>
        {
            // 创建农历日历对象
            var chineseCalendar = new ChineseLunisolarCalendar();
            // 获取当前日期
            var today = DateTime.Now;
            // 获取农历年份
            var lunarYear = chineseCalendar.GetYear(today);
            // 获取农历月份
            var lunarMonth = chineseCalendar.GetMonth(today);
            // 获取农历日期
            var lunarDay = chineseCalendar.GetDayOfMonth(today);
            // 获取农历纪年
            var chineseEra = GetChineseEra(lunarYear);

            // 将农历日期转换为中文
            var lunarDate = $"农历 {chineseEra}年{ConvertToChinese(lunarMonth)}月{ConvertToChinese(lunarDay)}日";
            // 在UI线程中更新农历日期
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