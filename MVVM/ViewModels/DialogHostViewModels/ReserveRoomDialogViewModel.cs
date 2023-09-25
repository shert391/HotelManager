using DevExpress.Mvvm;
using HotelManager.MVVM.Models.DataContract;
using HotelManager.MVVM.Models.Services;
using HotelManager.MVVM.Models.Services.RoomServices;
using HotelManager.MVVM.Utils;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;

namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

public class ReserveRoomDialogViewModel : AbstractDialogManagerViewModel, IConfigurable<Room>
{
    public ObservableCollection<People> NewPeoples { get; } = new();

    public int RemainPlaces { get; set; }
    public DateTime EndData { get; set; }
    public Room? TargetRoom { get; set; }

    public ICommand AddPeopleCommand { get; }
    public ICommand CancelCommand { get; }

    public ReserveRoomDialogViewModel()
    {
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
    public void Configurate(Room room)
    {
        TargetRoom = room;
        RemainPlaces = room.MaxPeoples;
    }
}