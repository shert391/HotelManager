using HotelManager.MVVM.Models.Builders;
using HotelManager.MVVM.Models.DataContract;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace HotelManager.MVVM.Models.Services.RoomService;

public interface IRoomService
{
    public void SubscribeChange(NotifyCollectionChangedEventHandler subscriber);
    public void AddRoom(Room newRoom, RoomServiceValidatorConfig config);
    public void EditRoom(Room newRoomDefinition, RoomServiceValidatorConfig config);
    public void DeleteRoom(int roomNumber);
    public ReadOnlyObservableCollection<Room> GetRooms();
    public ReadOnlyObservableCollection<Room> Find(int roomNumber);
}