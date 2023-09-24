using System.Collections.ObjectModel;
using HotelManager.MVVM.Models.DataContract;
using HotelManager.MVVM.Models.Others;

namespace HotelManager.MVVM.Models.Services.RoomServices;

public class RoomServiceValidator : ValidatorBase
{
    private readonly ReadOnlyObservableCollection<Room> _rooms;
    public RoomServiceValidator(ReadOnlyObservableCollection<Room> rooms) => _rooms = rooms;
    
    public bool CanAddRoom(Room newRoom)
    {
        if (!DefaultValidate(newRoom, typeof(RoomValidator)))
            return false;

        if (_rooms.All(x => x.Number != newRoom.Number)) return true;

        ErrorHandler("Комната уже существует!");
        return false;
    }

    public bool CanEditRoom(Room newRoom) => DefaultValidate(newRoom, typeof(RoomValidator));

    public bool CanReserved(Reservation reservation) => DefaultValidate(reservation, typeof(ReservationValidator));
}