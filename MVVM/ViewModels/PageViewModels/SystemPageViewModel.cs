using DevExpress.Mvvm;
using System.Windows.Input;
using HotelManager.MVVM.Models.Services;
using HotelManager.MVVM.Utils;
using HotelManager.MVVM.ViewModels.DialogHostViewModels;
using HotelManager.MVVM.Models.DataContract;
using System.Collections.Specialized;
using HotelManager.MVVM.Models.Services.RoomServices;

namespace HotelManager.MVVM.ViewModels.PageViewModels;

public class SystemPageViewModel : AbstractAppManagerViewModel
{
    public ICommand AddRoomCommand { get; }

    public ICommand ReserveRoomCommand { get; }

    public ICommand FindRoomCommand { get; }

    public ICommand EditRoomCommand { get; }

    public ICommand DeleteRoomCommand { get; }

    public ICommand GenerateRoomsCommand { get; }

    public int? NumberRoomTargetFind { get; set; }

    public SystemPageViewModel()
    {
        GenerateRoomsCommand = new DelegateCommand(() => TestService.GenerateTestRooms(30, 100, 3000, 100000));

        AddRoomCommand = new DelegateCommand(DialogHostController.Show<RoomCreatorDialogViewModel>);
        EditRoomCommand = new DelegateCommand<Room>(DialogHostController.Show<RoomEditorDialogViewModel, Room>);
        ReserveRoomCommand = new DelegateCommand<Room>(DialogHostController.Show<ReserveRoomDialogViewModel, Room>);

        DeleteRoomCommand = new DelegateCommand<int>(DeleteRoom);
        FindRoomCommand = new DelegateCommand<string>(FindRoom);
    }

    public void FindRoom(string text)
    {
        Rooms = !string.IsNullOrEmpty(text) ? RoomService.Find(int.Parse(text)) : RoomService.GetRooms();
    }

    public void DeleteRoom(int roomNumber)
    {
        DialogHostController.ShowMessageBoxConfirmation(
            () => RoomService.DeleteRoom(roomNumber),
            $"Вы точно хотите удалить комнату({roomNumber})?");
    }

    protected override void OnRoomCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        Rooms = NumberRoomTargetFind is not null ? RoomService.Find((int)NumberRoomTargetFind) : RoomService.GetRooms();
    }
}

