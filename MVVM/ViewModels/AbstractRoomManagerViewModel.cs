using System.Collections.ObjectModel;
using HotelManager.MVVM.Models.DataContract;
using HotelManager.MVVM.Models.Services;
using HotelManager.MVVM.Models.Services.RoomService;

namespace HotelManager.MVVM.ViewModels;

public abstract class AbstractRoomManagerViewModel : BaseViewModel
{
    protected readonly IRoomService RoomService;
    protected readonly ITestService TestService;
    public ReadOnlyObservableCollection<Room> Rooms { get; set; }
    protected AbstractRoomManagerViewModel(IRoomService roomService, ITestService testService)
    {
        RoomService = roomService;
        TestService = testService;
        Rooms = roomService.GetRooms();
        roomService.RoomCollectionChanged += OnRoomCollectionChanged;
    }

    protected virtual void OnRoomCollectionChanged() { }
}