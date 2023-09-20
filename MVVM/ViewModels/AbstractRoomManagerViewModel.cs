using System.Collections.ObjectModel;
using HotelManager.MVVM.Models.DataContract;
using HotelManager.MVVM.Models.Services;

namespace HotelManager.MVVM.ViewModels;

public abstract class AbstractRoomManagerViewModel : BaseViewModel
{
    protected readonly IRoomService RoomService;
    public ReadOnlyObservableCollection<Room> Rooms { get; set; }
    protected AbstractRoomManagerViewModel(IRoomService roomService)
    {
        RoomService = roomService;
        Rooms = roomService.GetRooms();
    }
}