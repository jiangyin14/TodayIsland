using ClassIsland.Core.Abstractions;
using ClassIsland.Core.Attributes;
using ClassIsland.Core.Extensions.Registry;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TodayIsland;

[PluginEntrance]
public class Plugin : PluginBase
{
    public override async void Initialize(HostBuilderContext context, IServiceCollection services)
    {
        services.AddComponent<WeekNumberTodayControl>();
        services.AddComponent<WeekOddTodayControl>();
        services.AddComponent<LunarDateControl>();
        services.AddComponent<RiseFallTimeControl>();
        services.AddComponent<NextHolidayDateControl>();
        services.AddComponent<NextHolidayDateEasierControl>();

        await LoadAndSaveNextHolidayData();
    }

    private async Task LoadAndSaveNextHolidayData()
    {
        try
        {
            Console.WriteLine("[TodayIsland][NextHoliday] 尝试从 api 获取节假日数据...");
            using (var httpClient = new HttpClient())
            {
                var url = "https://api.3r60.top/v1/holiday/json.php";
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                using (var responseStream = await response.Content.ReadAsStreamAsync())
                using (var streamReader = new StreamReader(responseStream))
                {
                    var responseBody = streamReader.ReadToEnd();
                    // 保存数据
                    File.WriteAllText("nextHolidayConfig.json", responseBody);
                    Console.WriteLine("[TodayIsland][NextHoliday] 保存节假日数据");
                }
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"[TodayIsland][NextHoliday] 请求错误: {e.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"[TodayIsland][NextHoliday] 未知错误: {e.Message}");
        }
    }
}