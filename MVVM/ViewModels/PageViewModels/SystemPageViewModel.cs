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

    public ICommand GenerateRoomsCommand { get; }

    public SystemPageViewModel(IRoomService roomService, ITestService testService) : base(roomService, testService)
    {
        GenerateRoomsCommand = new DelegateCommand(() => TestService.GenerateTestRooms(30, 100, 3000, 100000));
        AddRoomCommand = new DelegateCommand(DialogHostController.Show<RoomCreatorViewModel> );
        FindRoomCommand = new DelegateCommand<string>(FindRoom);
    }

    public void FindRoom(string text)
    {
        Rooms = !string.IsNullOrEmpty(text) ? RoomService.Find(int.Parse(text)) : RoomService.GetRooms();
    }
}

