using HotelManager.InitApp;

namespace HotelManager.MVVM.Models.Services;

public static class RuntimeUpdater
{
    public static event Action? Update;
    private static Dictionary<Type, Thread> _customUpdaters = new();
    
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

    public static void CreateUpdater(Type initiator, int delay, Action notified)
    {
        _customUpdaters.Add(initiator, new Thread(() =>
        {
            try
            {
                while (true)
                {
                    Thread.Sleep(delay);
                    App.ExecuteFromMainThread(() => notified());
                }
            }
            catch (ThreadInterruptedException ex) { }
        }));
        _customUpdaters[initiator].Start();
    }
    
    public static void DeleteUpdater(Type initiator)
    {
        _customUpdaters[initiator].Interrupt();
        _customUpdaters.Remove(initiator);
    }
}