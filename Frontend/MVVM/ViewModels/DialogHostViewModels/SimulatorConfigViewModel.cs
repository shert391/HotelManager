namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;
public class SimulatorConfigViewModel : AbstractDialogViewModel
{
    public int MaxNumberApplicationPerHour { get; set; } = 35;
    public int NumberDayTestPeriod { get; set; } = 5;
    public int MaxPeriodReserved { get; set; } = 5;
    public int AddHoursPerTick { get; set; } = 3;
    public int UpdaterDelay { get; set; } = 3000;
}