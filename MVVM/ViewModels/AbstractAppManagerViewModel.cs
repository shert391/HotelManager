using System.Collections.ObjectModel;
using System.Collections.Specialized;
using HotelManager.InitApp;
using HotelManager.MVVM.Models.DataContract;
using HotelManager.MVVM.Models.Services;
using HotelManager.MVVM.Models.Services.RoomServices;

namespace HotelManager.MVVM.ViewModels;

public abstract class AbstractAppManagerViewModel : BaseViewModel
{
    protected readonly IRoomService RoomService = App.Resolve<IRoomService>();
    protected readonly ITestService TestService = App.Resolve<ITestService>();
    public ReadOnlyObservableCollection<Room> Rooms { get; set; }
    protected AbstractAppManagerViewModel()
    {
        Rooms = RoomService.GetRooms();
        RoomService.SubscribeChange(OnRoomCollectionChanged);
    }

    protected virtual void OnRoomCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) { }
}