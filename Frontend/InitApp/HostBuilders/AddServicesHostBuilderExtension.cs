using HotelManager.MVVM.Models;
using HotelManager.MVVM.Models.Services.DebugHelperService;
using HotelManager.MVVM.Models.Services.FinanceService;
using HotelManager.MVVM.Models.Services.PostmanService;
using HotelManager.MVVM.Models.Services.ReservationService;
using HotelManager.MVVM.Models.Services.RoomService;
using HotelManager.MVVM.Models.Services.StatisticService;
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
            
            servicesProvider.TryAddSingleton<IFinanceService, FinanceService>();
            servicesProvider.TryAddSingleton<IPostmanService, PostmanService>();
            servicesProvider.TryAddSingleton<IStatisticService, StatisticService>();
            servicesProvider.TryAddSingleton<IDebugHelperService, DebugHelperService>();
        });

        return hostBuilder;
    }
}