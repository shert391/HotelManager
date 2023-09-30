using System.Collections.Specialized;
using DataContract.Extensions;
using DataContract.ViewModelsDto;

namespace HotelManager.MVVM.Models.Services.RoomService;

public interface IRoomService
{
    public event Action<RoomViewModel?, NotifyCollectionChangedAction, int, int>? RoomCollectionChanged;
    
    public ExtendedObservableCollection<RoomViewModel> GetRoomsForViewModel();
    
    public void DeleteRoom(int roomNumber);
    public void AddRoom(RoomViewModel newRoomDto);
    public void EditRoom(RoomViewModel newRoomDto);
}