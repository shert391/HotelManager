using HotelManager.MVVM.Models.DataContract;
using HotelManager.MVVM.Utils;
using System.Collections.ObjectModel;
using DevExpress.Mvvm.Native;

namespace HotelManager.MVVM.Models.Services;

public class RoomService : IRoomService
{
    private readonly ObservableCollection<Room> _rooms = new();
    private readonly ReadOnlyObservableCollection<Room> _roomsReadonly;

    public RoomService() => _roomsReadonly = new(_rooms);

    public ReadOnlyObservableCollection<Room> GetRooms() => _roomsReadonly;

    private void Validate(Room room, Action onSuccess, bool showDialogMessageOnError)
    {
        string? errorMessage = null;

        if (room.Number < Room.MinNumber || room.Number > Room.MaxNumber)
            errorMessage = $"Номер должен быть в диапазоне от {Room.MinNumber} до {Room.MaxNumber}";

        else if (_rooms.Any(x => room.Number == x.Number))
            errorMessage = "Комната уже существует!";

        else if (room.Price < Room.MinPrice || room.Price > Room.MaxPrice)
            errorMessage = $"Суточная цена должна быть в диапазоне от {Room.MinPrice} до {Room.MaxPrice}";

        else if (room.Number == 0 || !Enum.IsDefined(typeof(RoomType), room.Type) || room.Price == 0)
            errorMessage = "Все поля должны быть заполнены";

        if (!showDialogMessageOnError && errorMessage is not null)
            return;

        if (errorMessage is not null)
            DialogHostController.ShowMessageBox(errorMessage);
   
        onSuccess();
    }

    public void AddRoom(Room room, bool showDialogMessageOnError = true) => Validate(
        room, 
        () => _rooms.Add(room), 
        showDialogMessageOnError);

    public ReadOnlyObservableCollection<Room> Find(int number) =>
        new(_rooms.Where(room => room.Number == number).ToObservableCollection());
}