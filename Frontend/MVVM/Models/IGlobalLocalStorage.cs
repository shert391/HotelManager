using System.Collections.ObjectModel;
using DataContract.BusinessModels;

namespace HotelManager.MVVM.Models;

public interface IGlobalLocalStorage
{
    public ObservableCollection<Room> Rooms { get; }
    public ObservableCollection<Room> RoomsBackup { get; }
    
    /// <summary>
    /// Эта поле нужно исключительно для корректной работы симуляции, когда необходимо "ускорить время"
    /// </summary>
    public int AddHoursForTest { get; set; }
    public int GetRoomIndexInArray(int roomNumber);
    public Room? GetRoom(int roomNumber);
}