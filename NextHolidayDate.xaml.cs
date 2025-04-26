using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using ClassIsland.Core.Abstractions.Controls;
using ClassIsland.Core.Attributes;
using MaterialDesignThemes.Wpf;
using ClassIsland.Core;


namespace TodayIsland;

[ComponentInfo(
    "9F9D430D-CD37-1AF3-DBD3-0AB7E917FEC3",
    "下个节假日",
    PackIconKind.CalendarOutline,
    "在主界面显示距离下一个节假日的天数。"
)]
public partial class NextHolidayDateControl : ComponentBase
{
    public NextHolidayDateControl()
    {
        InitializeComponent();
        LoadNextHolidayAsync();
    }

private async void LoadNextHolidayAsync()
{
    try
    {
        using (var httpClient = new HttpClient())
        {
            var url = "https://api.smart-teach.cn/holiday.php";
            Console.WriteLine(url);
            
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            
            using (var responseStream = await response.Content.ReadAsStreamAsync())
            using (var streamReader = new StreamReader(responseStream))
            {
                var responseBody = streamReader.ReadToEnd();
                Console.WriteLine(responseBody);
                
                if (!IsValidJson(responseBody))
                {
                    Console.WriteLine("Invalid JSON response");
                    Dispatcher.Invoke(() => NextHolidayDate.Text = "加载节假日信息失败");
                    return;
                }
                
                var json = JObject.Parse(responseBody);
                
                if (json["message"] != null)
                {
                    var message = json["message"].ToString();
                    Dispatcher.Invoke(() => NextHolidayDate.Text = message);
                }
                else
                {
                    Dispatcher.Invoke(() => NextHolidayDate.Text = "未找到节假日信息");
                }
            }
        }
    }
    catch (HttpRequestException e)
    {
        Console.WriteLine($"Request error: {e.Message}");
        Dispatcher.Invoke(() => NextHolidayDate.Text = "加载节假日信息失败");
    }
    catch (Exception e)
    {
        Console.WriteLine($"Unexpected error: {e.Message}");
        Dispatcher.Invoke(() => NextHolidayDate.Text = "加载节假日信息时发生未知错误");
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