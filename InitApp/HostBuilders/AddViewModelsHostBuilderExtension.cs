using HotelManager.MVVM.ViewModels;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace HotelManager.InitApp.HostBuilders;

public static class AddViewModelsHostBuilderExtension
{
    public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureServices(servicesProvider =>
        {
            servicesProvider.TryAddSingleton<MainWindowViewModel>();
            servicesProvider.TryAddSingleton<TestPageViewModel>();
            servicesProvider.TryAddSingleton<SystemPageViewModel>();
        });

        return hostBuilder;
    }
}

