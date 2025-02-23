using ClassIsland.Core;
using ClassIsland.Core.Abstractions.Controls;
using ClassIsland.Core.Attributes;
using MaterialDesignThemes.Wpf;

namespace TodayIsland;

[ComponentInfo(
    "F96923F9-9E30-A755-9AD3-6A0D7205A671",
    "今日周数",
    PackIconKind.CalendarOutline,
    "在主界面显示本周在学期中的周数。"
)]
public partial class WeekTodayControl : ComponentBase
{
    public WeekTodayControl()
    {
        InitializeComponent();
        LoadWeekTodayAsync();
    }

    private async void LoadWeekTodayAsync()
    {
        // 获取开始时间
        var startTime = (DateTime)((dynamic)AppBase.Current).Settings.SingleWeekStartTime;
        Console.WriteLine(startTime);
        var endTime = DateTime.Now;
        // 获取开始时间的星期一
        var startWeek = Convert.ToInt32(startTime.DayOfWeek);
        // 因为是以星期一为第一天，所以要判断weeknow等于0时，要向前推6天。
        startWeek = startWeek == 0 ? 7 - 1 : startWeek - 1;
        var daydiff = -1 * startWeek;
        var firstDay = startTime.AddDays(daydiff).ToString("yyyy-MM-dd");
        var startMonday = Convert.ToDateTime(firstDay);
        // 获取结束时间的星期日
        var lastWeek = Convert.ToInt32(endTime.DayOfWeek);
-        if (lastWeek != 0) daydiff1 = 7 - lastWeek;
        // 本周最后一天
        var lastDay = endTime.AddDays(daydiff1).ToString("yyyy-MM-dd");
        var lastSunday = Convert.ToDateTime(lastDay);
        var timeSpan = lastSunday - startMonday;
        // 返回总周数
        var calcResult = ""
        calcResult Convert.ToInt32(span1.Days + 1) / 7;
        Dispatcher.Invoke(() => WeekTodayControl.Text = calcResult);
    }
}