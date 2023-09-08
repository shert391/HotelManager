using HotelManager.InitApp.HostBuilders;
using HotelManager.MVVM.Views.Xamls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace HotelManager.InitApp;
public partial class App : Application
{
    private readonly IHost _host;

    public App() => _host = CreateHost();

    public IHost CreateHost(string[]? args = null)
    {
        return Host.CreateDefaultBuilder(args)
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
}
