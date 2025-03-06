using System;
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
            var locationId = (string)((dynamic)AppBase.Current).Settings.CityId;
            locationId = locationId.Split(':')[1]; // 获取城市 id

            var apiKey = "5ab54681917844da8e1fad7ee55f6f84";
            var url = $"https://devapi.qweather.com/v7/astronomy/sun?key={apiKey}&location={locationId}&date=20250306";

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode(); // Throws if not 200-299

                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);

                // Validate if the response is valid JSON
                if (!IsValidJson(responseBody))
                {
                    Console.WriteLine("Invalid JSON response");
                    Dispatcher.Invoke(() => RiseFallTime.Text = "加载日出日落时间失败");
                    return;
                }

                var json = JObject.Parse(responseBody);

                var sunrise = json["sunrise"].ToString();
                var sunset = json["sunset"].ToString();

                var riseFallTime = $"日出: {sunrise}, 日落: {sunset}";
                Dispatcher.Invoke(() => RiseFallTime.Text = riseFallTime);
            }
        }
        catch (HttpRequestException e)
        {
            // Log the error or display a message to the user
            Console.WriteLine($"Request error: {e.Message}");
            Dispatcher.Invoke(() => RiseFallTime.Text = "加载日出日落时间失败");
        }
        catch (Exception e)
        {
            // Handle other potential errors
            Console.WriteLine($"Unexpected error: {e.Message}");
            Dispatcher.Invoke(() => RiseFallTime.Text = "加载日出日落时间时发生未知错误");
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