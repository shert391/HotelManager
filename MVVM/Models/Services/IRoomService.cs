using HotelManager.MVVM.Models.DataContract;
using System.Collections.ObjectModel;

namespace HotelManager.MVVM.Models.Services;

public interface IRoomService
{
    public ReadOnlyObservableCollection<Room> GetRooms();
    public void AddRoom(Room room);
}