using DataContract.GlobalConstants;
using DataContract.ViewModelsDto;
using DevExpress.Mvvm;
using HotelManager.MVVM.Models.Services;
using HotelManager.MVVM.Utils;
using HotelManager.MVVM.ViewModels.DialogHostViewModels;
using System.Windows.Input;
using DataContract.ViewModelsDto.Messages;

namespace HotelManager.MVVM.ViewModels.PageViewModels;

public enum SimulatorStateViewModel
{
    Run,
    Stop,
}

public class SimulatorPageViewModel : AbstractRoomManagerViewModel
{
    public SimulatorStateViewModel State { get; private set; } = SimulatorStateViewModel.Stop;
    public decimal Profit { get; private set; }
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
    public int NoHandledApplication => _applications.Count;
    public int BusyRoomsCount => Rooms.Count(room => room.CurrentState == RoomState.Busy);
    public int FreeRoomsCount => Rooms.Count(room => room.CurrentState == RoomState.Free);
    public ICommand ShowSettingSystemCommand { get; }
    public ICommand StartCommand { get; }
    public ICommand StopCommand { get; }

    private int _currentHour;
    private readonly Random _random = new();
    private readonly List<ApplicationDto> _applications = new();
    private readonly SimulatorConfigViewModel _simulatorConfigViewModel;

    public SimulatorPageViewModel(SimulatorConfigViewModel simulatorConfigViewModel)
    {
        StopCommand = new DelegateCommand(Stop);
        StartCommand = new DelegateCommand(Start);
        _simulatorConfigViewModel = simulatorConfigViewModel;
        ShowSettingSystemCommand =
            new DelegateCommand(() => DialogHostController.ConcreteShow(simulatorConfigViewModel));
    }

    protected override void RequestPayment(NeedPaymentMessage needPaymentMessage)
    {
        if (State != SimulatorStateViewModel.Run) return;
        FinanceService.PayRoom(needPaymentMessage);
        Profit += needPaymentMessage.TotalPrice;
    }

    private void ResetStats()
    {
        Profit = 0;
        CurrentDay = 0;
        _currentHour = 0;
        _applications.Clear();
        HandledApplication = 0;
        RaisePropertiesChanged();
    }

    private void Start()
    {
        State = SimulatorStateViewModel.Run;
        RoomService.CreateBackup();
        FinanceService.CreateBackup();
        RuntimeUpdater.CreateUpdater(typeof(SimulatorPageViewModel), _simulatorConfigViewModel.UpdaterDelay, Tick);
        ResetStats();
    }

    private void Stop()
    {
        State = SimulatorStateViewModel.Stop;
        RuntimeUpdater.DeleteUpdater(typeof(SimulatorPageViewModel));
        RoomService.ReturnBackup();
        FinanceService.ReturnBackup();
        RaisePropertiesChanged();
        DialogHostController.ShowMessageBox("Тестирование завершено!");
    }

    private void Tick()
    {
        CurrentHour += _simulatorConfigViewModel.AddHoursPerTick;

        _applications.AddRange(DebugHelperService.GenerateApplications(_simulatorConfigViewModel.MaxNumberApplicationPerHour,
            _simulatorConfigViewModel.MaxPeriodReserved));

        foreach (var application in _applications.ToArray())
        {
            if (!ApplicationService.Handle(application))
                continue;

            HandledApplication++;
            _applications.Remove(application);
        }

        RaisePropertiesChanged();
        if (CurrentDay >= _simulatorConfigViewModel.NumberDayTestPeriod) Stop();
    }
}