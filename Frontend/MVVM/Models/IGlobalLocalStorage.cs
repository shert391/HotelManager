using System.Collections.ObjectModel;
using DataContract.BusinessModels;

namespace HotelManager.MVVM.Models;

public interface IGlobalLocalStorage
{
    public ObservableCollection<Room> Rooms { get; }

    public int GetRoomIndexInArray(int roomNumber);
    public Room? GetRoom(int roomNumber);
}