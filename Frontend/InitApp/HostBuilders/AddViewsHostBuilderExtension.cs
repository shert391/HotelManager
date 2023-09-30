using HotelManager.MVVM.ViewModels;
using HotelManager.MVVM.Views.Xamls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace HotelManager.InitApp.HostBuilders;

public static class AddViewsHostBuilderExtension
{
    public static IHostBuilder AddViews(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureServices(serviceCollection =>
        {
            serviceCollection.TryAddSingleton(serviceProvider => new MainWindow(serviceProvider.GetRequiredService<MainWindowViewModel>()));
        });

        return hostBuilder;
    }
}

