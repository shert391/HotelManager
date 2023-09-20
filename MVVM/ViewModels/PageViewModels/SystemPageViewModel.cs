using DevExpress.Mvvm;
using HotelManager.MVVM.Models.Services;
using HotelManager.MVVM.Utils;
using HotelManager.MVVM.ViewModels.DialogHostViewModels;
using System.Windows.Input;

namespace HotelManager.MVVM.ViewModels.PageViewModels;

public class SystemPageViewModel : AbstractRoomManagerViewModel
{
    public ICommand AddRoomCommand { get; }
    public ICommand FindRoomCommand { get; }

    public SystemPageViewModel(IRoomService roomService) : base(roomService)
    {
        AddRoomCommand = new DelegateCommand(() => DialogHostController.Show<RoomCreatorViewModel>() );
        FindRoomCommand = new DelegateCommand<string>(FindRoom);
    }

    public void FindRoom(string text)
    {
        Rooms = !string.IsNullOrEmpty(text) ? RoomService.Find(int.Parse(text)) : RoomService.GetRooms();
    }
}

