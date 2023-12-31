﻿using HotelManager.InitApp.HostBuilders;
using HotelManager.MVVM.Views.Xamls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using Microsoft.Extensions.Configuration;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using HotelManager.MVVM.Utils;

namespace HotelManager.InitApp;
public partial class App
{
    private static readonly IHost _host;
    private static readonly IConfiguration _configApp;

    static App()
    {
        _host = CreateHost();
        _configApp = _host.Services.GetRequiredService<IConfiguration>();
    }

    private static IHost CreateHost(string[]? args = null)
    {
        return Host.CreateDefaultBuilder(args)
                    .AddConfiguration()
                    .AddAutoMappers()
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

    public static void ExecuteFromMainThread(Action action) => Current.Dispatcher.Invoke(action);
    public static T? GetSetting<T>(string setting) where T : notnull => _configApp.GetValue<T>(setting);
    public static T Resolve<T>() where T : notnull => _host.Services.GetRequiredService<T>();
    public static T Resolve<T, TOption>(TOption option) where T : notnull, IConfigurable<TOption>
    {
        var service = _host.Services.GetRequiredService<T>();
        service.Configure(option);
        return service;
    }
}
