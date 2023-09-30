using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DataContract;

namespace HotelManager.InitApp.HostBuilders;

public static class AddAutoMappersHostBuilderExtension
{
    public static IHostBuilder AddAutoMappers(this IHostBuilder hostBuilder)
    {
        return hostBuilder.ConfigureServices(serviceProvider =>
        {
            serviceProvider.AddAutoMapper(typeof(AutoMapperProfile));
        });
    }
}