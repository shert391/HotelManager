using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace HotelManager.InitApp.HostBuilders;

public static class AddConfigurationsHostBuilderExtension
{
    public static IHostBuilder AddConfiguration(this IHostBuilder hostBuilder)
    {
        return hostBuilder.ConfigureAppConfiguration(configureBuilder =>
        {
            configureBuilder.AddJsonStream(Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("HotelManager.GlobalSettings.json")!);
        });
    }
}

