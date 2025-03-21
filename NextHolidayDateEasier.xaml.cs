using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using ClassIsland.Core.Abstractions.Controls;
using ClassIsland.Core.Attributes;
using MaterialDesignThemes.Wpf;
using ClassIsland.Core;
using System.Windows.Threading;

namespace TodayIsland;

[ComponentInfo(
    "9DAA84F5-3957-0610-8E7C-4E6EEFAD7FDD",
    "下个节假日（简洁）",
    PackIconKind.CalendarOutline,
    "在主界面显示距离下一个节假日的天数（简洁版）。"
)]
public partial class NextHolidayDateEasierControl : ComponentBase
{
    public NextHolidayDateEasierControl()
    {
        InitializeComponent();
        LoadNextHolidayEasierAsync();
    }

    private async void LoadNextHolidayEasierAsync()
    {
        try
        {
            if (File.Exists("nextHolidayConfig.json"))
            {
                var responseBody = await File.ReadAllTextAsync("nextHolidayConfig.json");

                if (!IsValidJson(responseBody))
                {
                    Console.WriteLine("[TodayIsland][NextHoliday] Invalid JSON response");
                    Dispatcher.Invoke(() => NextHolidayDateEasier.Text = "加载节假日信息失败");
                    return;
                }

                var json = JObject.Parse(responseBody);

                if (json["next_holiday"] != null)
                {
                    var nextHoliday = json["next_holiday"];
                    var name = nextHoliday["name"]?.ToString();
                    var countdown = nextHoliday["countdown"]?.ToString();

                    if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(countdown))
                    {
                        var formattedText = $"{name} {countdown}天";
                        Dispatcher.Invoke(() => NextHolidayDateEasier.Text = formattedText);
                    }
                    else
                    {
                        Dispatcher.Invoke(() => NextHolidayDateEasier.Text = "节假日信息未找到");
                    }
                }
                else
                {
                    Dispatcher.Invoke(() => NextHolidayDateEasier.Text = "节假日信息未找到");
                }
            }
            else
            {
                Dispatcher.Invoke(() => NextHolidayDateEasier.Text = "节假日配置文件未找到");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Unexpected error: {e.Message}");
            Dispatcher.Invoke(() => NextHolidayDateEasier.Text = "加载时发生未知错误");
        }
    }

    private bool IsValidJson(string strInput)
    {
        if (string.IsNullOrWhiteSpace(strInput)) return false;
        strInput = strInput.Trim();
        if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || // For object
            (strInput.StartsWith("[") && strInput.EndsWith("]")))   // For array
        {
            try
            {
                var obj = JToken.Parse(strInput);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        return false;
    }
}