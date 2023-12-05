using HotelManager.InitApp.HostBuilders;
using HotelManager.MVVM.Views.Xamls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace HotelManager.InitApp;
public partial class App : Application
{
    private static readonly IHost _host;

    static App() => _host = CreateHost();

    private static IHost CreateHost(string[]? args = null)
    {
        return Host.CreateDefaultBuilder(args)
                    .AddViewModels()
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

    public static T Resolve<T>() where T : notnull => _host.Services.GetRequiredService<T>();
}
