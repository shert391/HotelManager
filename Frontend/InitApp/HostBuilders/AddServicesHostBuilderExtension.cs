using HotelManager.MVVM.Models;
using HotelManager.MVVM.Models.Services.ReservationService;
using HotelManager.MVVM.Models.Services.RoomService;
using HotelManager.MVVM.Models.Services.TestService;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace HotelManager.InitApp.HostBuilders;

public static class AddServicesHostBuilderExtension
{
    public static IHostBuilder AddServices(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureServices(servicesProvider =>
        {
            servicesProvider.TryAddSingleton<IGlobalLocalStorage, GlobalLocalStorage>();
            
            servicesProvider.TryAddSingleton<IReservationService, ReservationService>();
            servicesProvider.TryAddSingleton<IReservationServiceValidator, ReservationServiceValidator>();
            
            servicesProvider.TryAddSingleton<IRoomService, RoomService>();
            servicesProvider.TryAddSingleton<IRoomServiceValidator, RoomServiceValidator>();
            
            servicesProvider.TryAddSingleton<ITestService, TestService>();
        });

        return hostBuilder;
    }
}