using DevExpress.Mvvm;
using HotelManager.MVVM.Models.DataContract;
using HotelManager.MVVM.Models.Services;
using HotelManager.MVVM.Models.Services.RoomServices;
using HotelManager.MVVM.Utils;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

public class ReserveRoomDialogViewModel : AbstractDialogManagerViewModel, IConfigurable<Room>
{
    public ObservableCollection<People> NewPeoples { get; } = new();

    public DateTime EndData { get; set; }
    public Room TargetRoom { get; set; } = new();

    public ICommand AddPeopleCommand { get; }
    public ICommand CancelCommand { get; }

    public ReserveRoomDialogViewModel()
    {
        CancelCommand = new DelegateCommand(DialogHostController.Close);
        AddPeopleCommand = new DelegateCommand(AddPeople);
    }

    public void DelegateCommandAddPeople(People newPeople)
    {
        NewPeoples.Add(newPeople);
        DialogHostController.ShowMessageBoxInformation("Жилец успешно добавлен!");
    }
    
    public void AddPeople()
    {
        DialogHostController.Show<PeopleCreatorDialogViewModel, ICommand<People>>(
            new DelegateCommand<People>(DelegateCommandAddPeople));
    }

    public void Configurate(Room room) => TargetRoom = room;
}