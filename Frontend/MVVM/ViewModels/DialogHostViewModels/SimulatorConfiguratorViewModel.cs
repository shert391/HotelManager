using DataContract.GlobalConstants;

namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

public class SimulatorConfiguratorViewModel : AbstractDialogViewModel
{
    public int MaxNumberApplicationInTick { get; set; } = SimulatorSettingsConstants.MinNumberApplicationInTick + 10;
    public int NumberDayTestPeriod { get; set; } = 5;
}