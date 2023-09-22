using HotelManager.MVVM.Models.Builders;
using HotelManager.MVVM.Models.DataContract;
using System.Collections.ObjectModel;

namespace HotelManager.MVVM.Models.Services.RoomService;

public interface IRoomService
{
    public event Action RoomCollectionChanged;
    public void AddRoom(Room newRoom, RoomServiceValidatorConfig config);
    public void EditRoom(Room newRoomDefinition, RoomServiceValidatorConfig config);
    public void DeleteRoom(int roomNumber);
    public ReadOnlyObservableCollection<Room> GetRooms();
    public ReadOnlyObservableCollection<Room> Find(int roomNumber);
}