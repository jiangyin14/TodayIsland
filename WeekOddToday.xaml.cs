using ClassIsland.Core;
using ClassIsland.Core.Abstractions.Controls;
using ClassIsland.Core.Attributes;
using MaterialDesignThemes.Wpf;

namespace TodayIsland;

[ComponentInfo(
    "DDE5835B-C736-B6D2-CBE8-D478F8F327D3",
    "本周奇偶",
    PackIconKind.CalendarOutline,
    "在主界面显示本周在学期中的奇偶。"
)]
public partial class WeekOddTodayControl : ComponentBase
{
    public WeekOddTodayControl()
    {
        InitializeComponent();
        LoadWeekOddTodayAsync();
    }

    private async void LoadWeekOddTodayAsync()
    {
        
        // <summary>
        // 获取本周在学期中的周数
        // </summary> 
        
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
        var daydiff1 = lastWeek != 0 ? 7 - lastWeek : 0;
        // 本周最后一天
        var lastDay = endTime.AddDays(daydiff1).ToString("yyyy-MM-dd");
        var lastSunday = Convert.ToDateTime(lastDay);
        var timeSpan = lastSunday - startMonday;
        // 返回总周数
        var calcResult = (Convert.ToInt32(timeSpan.Days + 1) / 7).ToString();
        
        // <summary>
        // 获取本周在学期中的奇偶
        // </summary> 
        
        var weekNumber = Convert.ToInt32(calcResult);
        var weekOddToday = weekNumber % 2 == 0 ? "双周" : "单周";
        Dispatcher.Invoke(() => WeekOddToday.Text = weekOddToday);
        
        
        
    }
}