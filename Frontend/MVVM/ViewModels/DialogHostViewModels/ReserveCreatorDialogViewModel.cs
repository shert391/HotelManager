using DevExpress.Mvvm.Native;
using DevExpress.Mvvm;
using System.Windows.Input;
using HotelManager.MVVM.Utils;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using DataContract.DTO.ViewModels;
using HotelManager.MVVM.Models.Services.ReservationService;

namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

public class ReserveCreatorDialogViewModel : AbstractDialogViewModel
{
    public ObservableCollection<PeopleViewModel> NewPeoples { get; set; } = new();
    public DateTime EndData { get; set; } = DateTime.Now;

    private RoomViewModel? _targetRoomDto;

    public RoomViewModel? TargetRoomDto
    {
        set
        {
            _targetRoomDto = value;
            UpdateRemainPlaces();
        }
    }

    public int RemainPlaces { get; set; }
    
    
    private readonly IReservationService _reservationService;
    
    public ICommand ReserveCommand { get; }
    public ICommand AddPeopleCommand { get; }
    public ICommand EditPeopleCommand { get; }
    public ICommand DeletePeopleCommand { get; }

    public ReserveCreatorDialogViewModel(IReservationService reservationService)
    {
        _reservationService = reservationService;
        NewPeoples.CollectionChanged += OnPeopleCollectionChanged;

        ReserveCommand = new DelegateCommand(Reserve);
        AddPeopleCommand = new DelegateCommand(AddPeople);
        EditPeopleCommand = new DelegateCommand<PeopleViewModel>(EditPeople);
        DeletePeopleCommand = new DelegateCommand<PeopleViewModel>(DeletePeople);
    }

    private void OnPeopleCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) => UpdateRemainPlaces();

    private void UpdateRemainPlaces() => RemainPlaces = _targetRoomDto!.MaxPeoples - NewPeoples.Count;
    
    private void AddPeople()
    {
        if (RemainPlaces == 0)
        {
            DialogHostController.ShowMessageBox("Комната полностью заполнена!");
            return;
        }

        var viewModel = DialogHostController.ShowPull<PeopleCreatorDialogViewModel>();
        viewModel.OnSuccessPeopleValidated = newPeople =>
        {
            NewPeoples.Add(newPeople);
            DialogHostController.ShowMessageBox($"{newPeople.FullName} успешно добавлен!", 2);
        };
    }

    private void EditPeople(PeopleViewModel editablePeople)
    {
        var viewModel = DialogHostController.ShowPull<PeopleCreatorDialogViewModel>();
        viewModel.NewPeople = editablePeople.Clone();
        viewModel.CurrentViewState = DialogViewModelState.Edit;
        viewModel.OnSuccessPeopleValidated = newPeople =>
        {
            NewPeoples[NewPeoples.IndexOf(people => people == editablePeople)] = newPeople;
            DialogHostController.ShowMessageBox($"{newPeople.FullName} успешно изменён!", 2);
        };
    }

    private void DeletePeople(PeopleViewModel people)
    {
        DialogHostController.ShowMessageBoxConfirmation(() => NewPeoples.Remove(people),
            $"Вы точно хотите удалить {people.FullName}?");
    }

    private void Reserve()
    {
        _reservationService.Reserved(new() { Peoples = NewPeoples, EndData = EndData }, _targetRoomDto!.Number);
    }
}