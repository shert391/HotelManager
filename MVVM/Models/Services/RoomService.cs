using HotelManager.MVVM.Models.DataContract;
using System.Collections.ObjectModel;
using HotelManager.MVVM.Utils;

namespace HotelManager.MVVM.Models.Services;

public class RoomService : IRoomService
{
    private readonly ObservableCollection<Room> _rooms = new();
    private readonly ReadOnlyObservableCollection<Room> _roomsReadonly;

    public RoomService() => _roomsReadonly = new(_rooms);

    public ReadOnlyObservableCollection<Room> GetRooms() => _roomsReadonly;

    private void Validate(Room room, Action onSuccess)
    {
        string? errorMessage = null;

        if (room.Number < 0)
            errorMessage = "Намбер меньше нуля";

        if (errorMessage is null)
        {
            onSuccess();
            return;
        }
        
        DialogHostController.ShowMessageBox(errorMessage);
    }

    public void AddRoom(Room room) => Validate(room, () => _rooms.Add(room));
}