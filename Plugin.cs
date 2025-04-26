using ClassIsland.Core.Abstractions;
using ClassIsland.Core.Attributes;
using ClassIsland.Core.Extensions.Registry;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TodayIsland;

[PluginEntrance]
public class Plugin : PluginBase
{
    public override void Initialize(HostBuilderContext context, IServiceCollection services)
    {
        services.AddComponent<WeekNumberTodayControl>();
        services.AddComponent<WeekOddTodayControl>();
        services.AddComponent<LunarDateControl>();
        // services.AddComponent<RiseFallTimeControl>();
        services.AddComponent<NextHolidayDateControl>();
        services.AddComponent<NextHolidayDateEasierControl>();
    }
}