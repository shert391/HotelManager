using HotelManager.MVVM.Models.Services;
using HotelManager.MVVM.Models.Services.RoomService;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace HotelManager.InitApp.HostBuilders;

public static class AddServicesHostBuilderExtension
{
    public static IHostBuilder AddServices(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureServices(servicesProvider =>
        {
            servicesProvider.TryAddSingleton<IRoomService, RoomService>();
            servicesProvider.TryAddSingleton<ITestService, TestService>();
            servicesProvider.TryAddSingleton<IRoomServiceValidator, RoomServiceValidator>();
        });

        return hostBuilder;
    }
}