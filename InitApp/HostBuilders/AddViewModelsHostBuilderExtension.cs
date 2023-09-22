using HotelManager.MVVM.ViewModels;
using HotelManager.MVVM.ViewModels.DialogHostViewModels;
using HotelManager.MVVM.ViewModels.PageViewModels;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace HotelManager.InitApp.HostBuilders;

public static class AddViewModelsHostBuilderExtension
{
    public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureServices(servicesProvider =>
        {
            servicesProvider.TryAddSingleton<TestPageViewModel>();
            servicesProvider.TryAddSingleton<SystemPageViewModel>();
            servicesProvider.TryAddSingleton<MainWindowViewModel>();
            servicesProvider.TryAddSingleton<RoomCreatorViewModel>();
            servicesProvider.TryAddSingleton<RoomEditorViewModel>();
        });

        return hostBuilder;
    }
}

