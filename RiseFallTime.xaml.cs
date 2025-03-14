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
    "6DD56598-8C6E-C22D-D642-053EC1B0A1F1",
    "日月起落时间",
    PackIconKind.CalendarOutline,
    "在主界面显示本周在学期中的周数。"
)]
public partial class RiseFallTimeControl : ComponentBase
{
    public RiseFallTimeControl()
    {
        InitializeComponent();
        LoadRiseFallTimeAsync();
    }

private async void LoadRiseFallTimeAsync()
{
    try
    {
        // 获取城市 id
        var locationId = (string)((dynamic)AppBase.Current).Settings.CityId;
        locationId = locationId.Split(':')[1]; // 获取城市 id

        var apiKey = "5ab54681917844da8e1fad7ee55f6f84";
        var today = DateTime.Now.ToString("yyyyMMdd");
        var url = $"https://devapi.qweather.com/v7/astronomy/sun?key={apiKey}&location={locationId}&date={today}";

        using (var httpClient = new HttpClient())
        {
            Console.WriteLine(url);
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            using (var responseStream = await response.Content.ReadAsStreamAsync())
            using (var deflateStream = new GZipStream(responseStream, CompressionMode.Decompress))
            using (var streamReader = new StreamReader(deflateStream))
            {
                var responseBody = streamReader.ReadToEnd();
                Console.WriteLine(responseBody);

                if (!IsValidJson(responseBody))
                {
                    Console.WriteLine("Invalid JSON response");
                    Dispatcher.Invoke(() => RiseFallTime.Text = "加载日出日落时间失败");
                    return;
                }

                var json = JObject.Parse(responseBody);

                var sunrise = json["sunrise"].ToString();
                var sunset = json["sunset"].ToString();

                var formattedSunrise = FormatTime(sunrise);
                var formattedSunset = FormatTime(sunset);

                var riseFallTime = $"日出: {formattedSunrise}, 日落: {formattedSunset}";
                Dispatcher.Invoke(() => RiseFallTime.Text = riseFallTime);
            }
        }
    }
    catch (HttpRequestException e)
    {
        Console.WriteLine($"Request error: {e.Message}");
        Dispatcher.Invoke(() => RiseFallTime.Text = "加载日出日落时间失败");
    }
    catch (Exception e)
    {
        Console.WriteLine($"Unexpected error: {e.Message}");
        Dispatcher.Invoke(() => RiseFallTime.Text = "加载日出日落时间时发生未知错误");
    }
}

private string FormatTime(string timeStr)
{
    var dateTime = DateTime.Parse(timeStr);
    var period = dateTime.Hour < 12 ? "早上" : "晚上";
    var formattedTime = dateTime.ToString("HH:mm");
    return $"{period} {formattedTime}";
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