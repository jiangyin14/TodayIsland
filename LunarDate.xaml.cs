using System.Net.Http;
using System.Threading.Tasks;
using ClassIsland.Core.Controls;
using System.Windows;
using ClassIsland.Core.Abstractions.Controls;
using ClassIsland.Core.Attributes;
using MaterialDesignThemes.Wpf;
using System.Windows.Controls;
using ClassIsland.Core;
using Microsoft.VisualBasic.Logging;

namespace TodayIsland
{
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
            LoadWeekTodayAsync();
        }

        private async void LoadWeekTodayAsync()
        {
            var startTime = (DateTime)((dynamic)AppBase.Current).Settings.SingleWeekStartTime;
            Console.WriteLine(startTime);
        }
    }
}