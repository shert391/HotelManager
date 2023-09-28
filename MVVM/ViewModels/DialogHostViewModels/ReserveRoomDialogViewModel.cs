using DevExpress.Mvvm;
using System.Windows.Input;
using HotelManager.MVVM.Utils;
using HotelManager.MVVM.Models.Builders;
using HotelManager.MVVM.Models.DataContract;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

public class ReserveRoomDialogViewModel : AbstractDialogInteractiveViewModel, IConfigurable<Room>
{
    public ObservableCollection<People> NewPeoples { get; } = new();

    public int RemainPlaces { get; set; }
    public DateTime EndData { get; set; } = DateTime.Now;
    public Room? TargetRoom { get; set; }

    public ICommand CancelCommand { get; }
    public ICommand ReserveCommand { get; }
    public ICommand AddPeopleCommand { get; }
    public ICommand EditPeopleCommand { get; }
    public ICommand DeletePeopleCommand { get; }

    public ReserveRoomDialogViewModel()
    {
        NewPeoples.CollectionChanged += OnPeopleCollectionChanged;

        ReserveCommand = new DelegateCommand(Reserve);
        AddPeopleCommand = new DelegateCommand(AddPeople);
        EditPeopleCommand = new DelegateCommand<People>(EditPeople);
        DeletePeopleCommand = new DelegateCommand<People>(DeletePeople);
        CancelCommand = new DelegateCommand(DialogHostController.Close);
    }

    private void OnPeopleCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) =>
        RemainPlaces = TargetRoom!.MaxPeoples - NewPeoples.Count;

    private void AddPeople()
    {
        if (RemainPlaces == 0)
        {
            DialogHostController.ShowMessageBoxInformation("Комната полностью заполнена!");
            return;
        }

        DialogHostController.Show<PeopleCreatorDialogViewModel, PeopleCreatorDialogViewModel.Configuration>(
            new PeopleCreatorDialogViewModel.Configuration()
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
        DialogHostController.Show<PeopleCreatorDialogViewModel, PeopleCreatorDialogViewModel.Configuration>(
            new PeopleCreatorDialogViewModel.Configuration()
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
        DialogHostController.ShowMessageBoxConfirmation(() => NewPeoples.Remove(people), $"Вы точно хотите удалить {people.FullName}?");
    }

    private void Reserve()
    {
        RoomService.ReservedRoom(new()
            {
                Peoples = NewPeoples,
                EndData = EndData,
            },
            DefaultValidatorConfigBuilder.Create().AddShowMessageBoxSuccessError("Комната забронирована!", true)
                .Build(),
            TargetRoom!.Number);
    }

    public void Configurate(Room room)
    {
        TargetRoom = room;
        RemainPlaces = room.MaxPeoples;
    }
}