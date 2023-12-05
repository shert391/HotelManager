using System.Collections.ObjectModel;
using DevExpress.Mvvm;
using System.Windows.Input;
using DataContract.DTO.Messages;
using DataContract.DTO.ViewModels;
using DevExpress.Mvvm.Native;
using HotelManager.MVVM.Utils;
using HotelManager.MVVM.Models.Services;
using HotelManager.MVVM.ViewModels.DialogHostViewModels;


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
                _nextSpawnApplication = 0;
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
    public ICommand NoHandledApplicationViewCommand { get; }
    public ICommand ShowSettingSystemCommand { get; }
    public ICommand StartCommand { get; }
    public ICommand StopCommand { get; }

    private int _currentHour;
    private int _nextSpawnApplication;
    private readonly Random _random = new();
    private readonly List<ApplicationViewModel> _applications = new();
    private readonly SimulatorConfigViewModel _simulatorConfigViewModel;

    public SimulatorPageViewModel(SimulatorConfigViewModel simulatorConfigViewModel)
    {
        StopCommand = new DelegateCommand(Stop);
        StartCommand = new DelegateCommand(Start);
        _simulatorConfigViewModel = simulatorConfigViewModel;
        ShowSettingSystemCommand =
            new DelegateCommand(() => DialogHostController.ConcreteShow(simulatorConfigViewModel));
        NoHandledApplicationViewCommand = new DelegateCommand(() => DialogHostController.Show<NoHandledApplicationViewModel, ObservableCollection<ApplicationViewModel>>(_applications.ToObservableCollection()));
    }

    protected override void RequestPayment(NeedPaymentMessage needPaymentMessage)
    {
        base.RequestPayment(needPaymentMessage);
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
        _nextSpawnApplication = 0;
    }

    private void Start()
    {
        if (Rooms.Count != 0)
        {
            State = SimulatorStateViewModel.Run;
            RoomService.CreateBackup();
            FinanceService.CreateBackup();
            _nextSpawnApplication += _random.Next(_simulatorConfigViewModel.IntervalSpawnApplication.Min,
                _simulatorConfigViewModel.IntervalSpawnApplication.Max);
            RuntimeUpdater.CreateUpdater(typeof(SimulatorPageViewModel), _simulatorConfigViewModel.UpdaterDelay, Tick);
            ResetStats();
            _nextSpawnApplication += _random.Next(_simulatorConfigViewModel.IntervalSpawnApplication.Min,
                _simulatorConfigViewModel.IntervalSpawnApplication.Max);
            return;
        }

        DialogHostController.ShowMessageBox("Ошибка: для запуска тестирования нужна хотя бы одна комната!");
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

        if (CurrentHour < _nextSpawnApplication)
            return;

        _nextSpawnApplication = CurrentHour + _random.Next(_simulatorConfigViewModel.IntervalSpawnApplication.Min,
            _simulatorConfigViewModel.IntervalSpawnApplication.Max);

        _applications.AddRange(DebugHelperService.GenerateApplications(2, _simulatorConfigViewModel.IntervalPeriodReserved));

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