using HotelManager.MVVM.Views.Xamls;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace HotelManager.InitApp.HostBuilders;

public static class AddViewHostBuilderExtension
{
    public static IHostBuilder AddViews(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureServices(serviceCollection =>
        {
            serviceCollection.TryAddSingleton(serviceProvider => new MainWindow());
        });

        return hostBuilder;
    }
}

