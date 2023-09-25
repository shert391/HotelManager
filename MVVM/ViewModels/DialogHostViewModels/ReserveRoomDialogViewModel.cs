using DevExpress.Mvvm;
using System.Windows.Input;
using HotelManager.MVVM.Utils;
using HotelManager.MVVM.Models.Builders;
using HotelManager.MVVM.Models.DataContract;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

public class ReserveRoomDialogViewModel : AbstractDialogManagerViewModel, IConfigurable<Room>
{
    public ObservableCollection<People> NewPeoples { get; } = new();

    public int RemainPlaces { get; set; }
    public DateTime EndData { get; set; } = DateTime.Now;
    public Room? TargetRoom { get; set; }

    public ICommand AddPeopleCommand { get; }
    public ICommand ReserveCommand { get; }
    public ICommand CancelCommand { get; }

    public ReserveRoomDialogViewModel()
    {
        ReserveCommand = new DelegateCommand(Reserve);
        AddPeopleCommand = new DelegateCommand(AddPeople);
        NewPeoples.CollectionChanged += OnPeopleCollectionChanged;
        CancelCommand = new DelegateCommand(DialogHostController.Close);
    }

    public void OnPeopleCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) => RemainPlaces = TargetRoom!.MaxPeoples - NewPeoples.Count;
    public void AddPeople()
    {
        if (RemainPlaces == 0)
            DialogHostController.ShowMessageBoxInformation("Комната полностью заполнена!");
        else
            DialogHostController.Show<PeopleCreatorDialogViewModel, ObservableCollection<People>>(NewPeoples);
    }

    public void Reserve()
    {
        RoomService.ReservedRoom(new()
        {
            Peoples = NewPeoples,
            EndData = EndData,
        }, DefaultValidatorConfigBuilder.Create().AddShowMessageBoxSuccessError("Комната забронирована!", true).Build(), TargetRoom!.Number);
    }

    public void Configurate(Room room)
    {
        TargetRoom = room;
        RemainPlaces = room.MaxPeoples;
    }
}