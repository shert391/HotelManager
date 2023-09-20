using HotelManager.InitApp.HostBuilders;
using HotelManager.MVVM.Views.Xamls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using Microsoft.Extensions.Configuration;

namespace HotelManager.InitApp;
public partial class App
{
    private static readonly IHost _host;
    private static readonly IConfiguration _configurationApp;
    
    static App()
    {
        _host = CreateHost();
        _configurationApp = _host.Services.GetRequiredService<IConfiguration>();
    }

    private static IHost CreateHost(string[]? args = null)
    {
        return Host.CreateDefaultBuilder(args)
                    .AddConfiguration()
                    .AddViewModels()
                    .AddServices()
                    .AddViews()
                    .Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        _host.Start();

        Window window = _host.Services.GetRequiredService<MainWindow>();
        window.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await _host.StopAsync();
        _host.Dispose();

        base.OnExit(e);
    }

    public static T? GetRoomSetting<T>(string setting) => _configurationApp.GetSection("RoomSettings").GetValue<T>(setting); 
    public static T Resolve<T>() where T : notnull => _host.Services.GetRequiredService<T>();
}
