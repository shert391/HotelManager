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
            servicesProvider.TryAddSingleton<PayRoomViewModel>();
            servicesProvider.TryAddSingleton<TestPageViewModel>();
            servicesProvider.TryAddSingleton<PayHistoryViewModel>();
            servicesProvider.TryAddSingleton<SystemPageViewModel>();
            servicesProvider.TryAddSingleton<MainWindowViewModel>();
            servicesProvider.TryAddSingleton<RoomEditorDialogViewModel>();
            servicesProvider.TryAddSingleton<RoomCreatorDialogViewModel>();
            servicesProvider.TryAddTransient<PeopleCreatorDialogViewModel>();
            servicesProvider.TryAddTransient<ReserveCreatorDialogViewModel>();
        });

        return hostBuilder;
    }
}

