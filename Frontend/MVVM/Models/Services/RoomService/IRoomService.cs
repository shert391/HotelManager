using System.Collections.ObjectModel;
using System.Collections.Specialized;
using DataContract.DTO.MappingEntities;
using DataContract.DTO.ViewModels;
using DataContract.Extensions;

namespace HotelManager.MVVM.Models.Services.RoomService;

public interface IRoomService
{
    public event Action<RoomViewModel?, NotifyCollectionChangedAction, int, int>? RoomCollectionChanged;
    
    public ObservableCollection<RoomViewModel> GetRoomsForViewModel();

    public void AddRoom(RoomViewModel newRoomDto);
    public void EditRoom(RoomViewModel newRoomDto);
    public void DeleteRoom(int roomNumber);
    public void CreateBackup();
    public void ReturnBackup();
    public void ClearAll();
}