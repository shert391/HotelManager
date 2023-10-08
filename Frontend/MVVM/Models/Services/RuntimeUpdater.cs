using HotelManager.InitApp;

namespace HotelManager.MVVM.Models.Services;

public static class RuntimeUpdater
{
    public static event Action? Update;

    static RuntimeUpdater()
    {
        var rootThread = new Thread(() =>
        {
            while (true)
            {
                Thread.Sleep(App.GetSetting<int>("UpdaterDelay"));
                App.ExecuteFromMainThread(() => Update?.Invoke());
            }
        });
        rootThread.Start();
    }
}