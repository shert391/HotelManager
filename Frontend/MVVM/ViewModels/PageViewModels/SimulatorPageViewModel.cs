using System.Windows.Input;
using DataContract.ViewModelsDto;
using DevExpress.Mvvm;
using HotelManager.MVVM.Utils;
using HotelManager.MVVM.ViewModels.DialogHostViewModels;

namespace HotelManager.MVVM.ViewModels.PageViewModels;

public class SimulatorPageViewModel : AbstractRoomManagerViewModel
{
    public int TotalRooms => Rooms.Count;
    public int BusyRoomsCount => Rooms.Count(room => room.CurrentState == RoomState.Busy);
    public int FreeRoomsCount => Rooms.Count(room => room.CurrentState == RoomState.Free);
    public ICommand ShowSettingSystemCommand { get; }
    public ICommand StartTestCommand { get; }
    public ICommand StopTestCommand { get; }

    private readonly SimulatorConfiguratorViewModel _configSimulatorConfiguratorViewModel;

    public SimulatorPageViewModel(SimulatorConfiguratorViewModel configSimulatorConfiguratorViewModel)
    {
        ShowSettingSystemCommand = new DelegateCommand(() => DialogHostController.ConcreteShow(configSimulatorConfiguratorViewModel));
    }
}

