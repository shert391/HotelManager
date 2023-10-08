using DataContract.GlobalConstants;
using DataContract.ViewModelsDto;
using DevExpress.Mvvm;
using HotelManager.MVVM.Models.Services;
using HotelManager.MVVM.Utils;
using HotelManager.MVVM.ViewModels.DialogHostViewModels;
using System.Windows.Input;
using DataContract.ViewModelsDto.Messages;

namespace HotelManager.MVVM.ViewModels.PageViewModels;

public class SimulatorPageViewModel : AbstractRoomManagerViewModel
{
    public decimal Profit { get; set; }
    public int TotalApplication { get; set; }
    public int HandledApplication { get; set; }
    public int CurrentDay { get; set; }
    public int CurrentHour
    {
        get => _currentHour;
        set
        {
            if (_currentHour >= 24)
            {
                CurrentDay++;
                _currentHour = 0;
                return;
            }
            var delta = value - _currentHour;
            DebugHelperService.AddHoursToStorageTime(delta);
            _currentHour += delta;
        }
    }
    public int TotalRooms => Rooms.Count;
    public int BusyRoomsCount => Rooms.Count(room => room.CurrentState == RoomState.Busy);
    public int FreeRoomsCount => Rooms.Count(room => room.CurrentState == RoomState.Free);
    public ICommand ShowSettingSystemCommand { get; }
    public ICommand StartSimulateCommand { get; }
    public ICommand StopSimulateCommand { get; }

    private int _currentHour;
    private readonly Random _random = new();
    private readonly List<ApplicationViewModel> _applications = new();
    private readonly SimulatorConfiguratorViewModel _simulatorConfiguratorViewModel;

    public SimulatorPageViewModel(SimulatorConfiguratorViewModel simulatorConfiguratorViewModel)
    {
        StopSimulateCommand = new DelegateCommand(Stop);
        StartSimulateCommand = new DelegateCommand(Start);
        _simulatorConfiguratorViewModel = simulatorConfiguratorViewModel;
        ShowSettingSystemCommand =
            new DelegateCommand(() => DialogHostController.ConcreteShow(simulatorConfiguratorViewModel));
    }

    protected override void RequestPayment(PayInformationDto payInformationDto)
    {
        FinanceService.PayRoom(payInformationDto);
    }

    private void Start()
    {
        RoomService.CreateBackup();
        RuntimeUpdater.Update += Tick;
    }

    private void Stop()
    {
        RuntimeUpdater.Update -= Tick;
        RoomService.ReturnBackup();
    }

    private void Tick()
    {
        CurrentHour++;
        
        _applications.AddRange(DebugHelperService.GenerateApplications(_random.Next(
            SimulatorSettingsConstants.MinNumberApplicationInTick,
            _simulatorConfiguratorViewModel.MaxNumberApplicationInTick)));
        
        foreach (var application in _applications.Where(application => ApplicationService.Handle(application)).ToArray())
        {
            HandledApplication++;
            _applications.Remove(application);
        }

        Profit = FinanceService.GetHotelRevenues();
        TotalApplication += _applications.Count;
        
        if(CurrentDay >= _simulatorConfiguratorViewModel.NumberDayTestPeriod) Stop();
    }
}