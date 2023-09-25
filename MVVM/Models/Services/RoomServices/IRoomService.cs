using System.Collections.ObjectModel;
using System.Collections.Specialized;
using HotelManager.MVVM.Models.Configs;
using HotelManager.MVVM.Models.DataContract;
using HotelManager.MVVM.Models.DataContract.Requests;

namespace HotelManager.MVVM.Models.Services.RoomServices;

public interface IRoomService
{
    public void ReservedRoom(Reservation reservation, IDefaultValidatorConfig validatorConfig, int targetRoomNumber);
    public void EditRoom(RoomChangeRequest roomChangeRequest, IDefaultValidatorConfig validatorConfig);
    public void AddRoom(Room newRoom, IDefaultValidatorConfig validatorConfig);
    public void DeleteRoom(int roomNumber);
    
    public void SubscribeChange(NotifyCollectionChangedEventHandler subscriber);
    public ReadOnlyObservableCollection<Room> GetRooms();
    public ReadOnlyObservableCollection<Room> FindAll(Func<Room, bool> expression);
}