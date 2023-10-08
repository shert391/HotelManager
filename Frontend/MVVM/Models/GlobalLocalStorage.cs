using System.Collections.ObjectModel;
using DataContract.BusinessModels;
using DevExpress.Mvvm.Native;

namespace HotelManager.MVVM.Models;

public class GlobalLocalStorage : IGlobalLocalStorage
{
    public ObservableCollection<Room> Rooms { get; } = new();
    public ObservableCollection<Room> RoomsBackup { get; } = new();
    public int AddHoursForTest { get; set; }
    public int GetRoomIndexInArray(int roomNumber) => Rooms.IndexOf(room => room.Number == roomNumber);
    public Room? GetRoom(int roomNumber) => Rooms.FirstOrDefault(room => room.Number == roomNumber);
}