using DevExpress.Mvvm;
using System.Windows.Input;
using HotelManager.MVVM.Utils;
using HotelManager.MVVM.Models.Builders;
using HotelManager.MVVM.Models.DataContract;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using HotelManager.MVVM.Models.Services.RoomServices;
using ReserveRoomDialogViewModelConfiguration =
    HotelManager.MVVM.ViewModels.DialogHostViewModels.ReserveCreatorDialogViewModel.Configuration;
using PeopleCreatorDialogViewModelConfiguration =
    HotelManager.MVVM.ViewModels.DialogHostViewModels.PeopleCreatorDialogViewModel.Configuration;

namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

public class ReserveCreatorDialogViewModel : AbstractDialogViewModel,
    IConfigurable<ReserveRoomDialogViewModelConfiguration>
{
    public class Configuration : BindableBase
    {
        public int MaxPeoples { get; init; }
        public bool IsStateEditing { get; init; }
        public int TargetRoomIndex { get; init; }
    }

    public ObservableCollection<People> NewPeoples { get; private set; } = new();
    public Configuration Config { get; private set; } = new();

    private IRoomService _roomService;
    
    public int RemainPlaces { get; set; }
    public DateTime EndData { get; set; }

    public ICommand CancelCommand { get; }
    public ICommand ReserveCommand { get; }
    public ICommand AddPeopleCommand { get; }
    public ICommand EditPeopleCommand { get; }
    public ICommand DeletePeopleCommand { get; }

    public ReserveCreatorDialogViewModel(IRoomService roomService)
    {
        _roomService = roomService;
        NewPeoples.CollectionChanged += OnPeopleCollectionChanged;

        ReserveCommand = new DelegateCommand(Reserve);
        AddPeopleCommand = new DelegateCommand(AddPeople);
        EditPeopleCommand = new DelegateCommand<People>(EditPeople);
        DeletePeopleCommand = new DelegateCommand<People>(DeletePeople);
        CancelCommand = new DelegateCommand(DialogHostController.Close);
    }

    private void OnPeopleCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) => UpdateRemainPlaces();

    private void UpdateRemainPlaces() => RemainPlaces = Config.MaxPeoples - NewPeoples.Count;
    
    private void AddPeople()
    {
        if (RemainPlaces == 0)
        {
            DialogHostController.ShowMessageBoxInformation("Комната полностью заполнена!");
            return;
        }

        DialogHostController.Show<PeopleCreatorDialogViewModel, PeopleCreatorDialogViewModelConfiguration>(
            new PeopleCreatorDialogViewModelConfiguration
            {
                OnSuccessfulValidation = newPeople =>
                {
                    NewPeoples.Add(newPeople);
                    DialogHostController.ShowMessageBoxInformation($"{newPeople.FullName} успешно добавлен!", 2);
                },
                IsStateEditing = false
            });
    }

    private void EditPeople(People editablePeople)
    {
        DialogHostController.Show<PeopleCreatorDialogViewModel, PeopleCreatorDialogViewModelConfiguration>(
            new PeopleCreatorDialogViewModelConfiguration()
            {
                OnSuccessfulValidation = newPeople =>
                {
                    NewPeoples[NewPeoples.IndexOf(editablePeople)] = newPeople;
                    DialogHostController.ShowMessageBoxInformation($"{newPeople.FullName} успешно изменён!", 2);
                },
                InitInputDefault = editablePeople,
                IsStateEditing = true
            });
    }

    private void DeletePeople(People people)
    {
        DialogHostController.ShowMessageBoxConfirmation(() => NewPeoples.Remove(people),
            $"Вы точно хотите удалить {people.FullName}?");
    }

    private void Reserve()
    {
        var newReservation = new Reservation()
        {
            Peoples = NewPeoples,
            EndData = EndData,
        };

        var message = !Config.IsStateEditing ? "Комната забронирована!" : "Бронь изменена!";
        
        _roomService.ReservedRoom(newReservation,
            DefaultValidatorConfigBuilder.Create().AddShowMessageBoxSuccessError(message, true)
                .Build(),
            Config.TargetRoomIndex);
    }

    public void Configurate(Configuration configuration)
    {
        Config = configuration;
        UpdateRemainPlaces();

        if (!Config.IsStateEditing)
        {
            EndData = DateTime.Now;
            return;
        }

        foreach (var people in _roomService.GetPeoplesInfo(Config.TargetRoomIndex))
            NewPeoples.Add(people.Clone());

        EndData = _roomService.GetReservationInfo(Config.TargetRoomIndex, reservation => reservation.EndData);

        _roomService.GetRooms();
    }
}