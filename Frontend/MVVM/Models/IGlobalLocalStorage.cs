using System.Collections.ObjectModel;
using DataContract.BusinessModels;

namespace HotelManager.MVVM.Models;

public interface IGlobalLocalStorage
{
    public ObservableCollection<Room> Rooms { get; }
    public ObservableCollection<Room> RoomsBackup { get; }
    public DateTime StorageTime { get; set; }
    public int GetRoomIndexInArray(int roomNumber);
    public Room? GetRoom(int roomNumber);
}