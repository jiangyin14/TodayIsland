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
            var startTime = (DateTime)((dynamic)AppBase.Current).Settings.SingleWeekStartTime;
            Console.WriteLine(startTime);
        }
    }
}