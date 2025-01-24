using ClassIsland.Core.Abstractions;
using ClassIsland.Core.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ClassIsland.Core.Extensions.Registry;

namespace TodayIsland
{
    [PluginEntrance]
    public class Plugin : PluginBase
    {
        public override void Initialize(HostBuilderContext context, IServiceCollection services)
        {
            services.AddComponent<WeekToday>();
            services.AddComponent<LunarDate>();
            services.AddComponent<RiseFallTime>();
        }
    }   
}
