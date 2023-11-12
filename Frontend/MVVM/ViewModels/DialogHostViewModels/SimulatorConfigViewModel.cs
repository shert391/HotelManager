using DataContract;

namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;
public class SimulatorConfigViewModel : AbstractDialogViewModel
{
    public Interval<int> IntervalSpawnApplication { get; set; } = new() { Min = 1, Max = 5 };
    public Interval<int> IntervalPeriodReserved { get; set; } = new() { Min = 1, Max = 2 };
    public int NumberDayTestPeriod { get; set; } = 12;
    public int AddHoursPerTick { get; set; } = 3;
    public int UpdaterDelay { get; set; } = 1000;
}