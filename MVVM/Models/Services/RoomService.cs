using HotelManager.MVVM.Models.DataContract;
using System.Collections.ObjectModel;
using DevExpress.Mvvm.Native;
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

        if (room.Number is < 1 or > 100)
            errorMessage = "Номер должен быть в диапазоне от 1 до 100";

        if (_rooms.Any(x => room.Number == x.Number))
            errorMessage = "Комната уже существует!";

        if (room.Price < 1000)
            errorMessage = "Цена должна быть > 1000";
        
        if (room.Number == 0 || !Enum.IsDefined(typeof(RoomType), room.Type) || room.Price == 0)
            errorMessage = "Все поля должны быть заполнены";

        if (errorMessage is null)
        {
            onSuccess();
            return;
        }
        
        DialogHostController.ShowMessageBox(errorMessage);
    }

    public void AddRoom(Room room) => Validate(room, () => _rooms.Add(room));
}