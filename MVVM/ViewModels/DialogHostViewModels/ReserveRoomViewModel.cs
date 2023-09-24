using DevExpress.Mvvm;
using HotelManager.MVVM.Models.DataContract;
using HotelManager.MVVM.Models.Services;
using HotelManager.MVVM.Models.Services.RoomServices;
using HotelManager.MVVM.Utils;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

public class ReserveRoomViewModel : AbstractRoomManagerViewModel, IConfigurable<Room>, IDialogViewModel
{
    public ObservableCollection<People> NewPeoples { get; } = new();

    public DateTime EndData { get; set; }
    public Room TargetRoom { get; set; } = new();

    public ICommand AddPeopleCommand { get; }
    public ICommand ReserveCommand { get; }
    public ICommand CancelCommand { get; }

    public ReserveRoomViewModel(IRoomService roomService, ITestService testService) : base(roomService, testService)
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
        DialogHostController.Show<PeopleCreatorViewModel, ICommand<People>>(
            new DelegateCommand<People>(DelegateCommandAddPeople));
    }

    public void Configurate(Room room) => TargetRoom = room;
}