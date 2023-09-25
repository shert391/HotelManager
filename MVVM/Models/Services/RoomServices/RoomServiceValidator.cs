using HotelManager.MVVM.Models.DataContract;
using HotelManager.MVVM.Models.Others;
using System.Collections.ObjectModel;

namespace HotelManager.MVVM.Models.Services.RoomServices;

public class RoomServiceValidator : ValidatorBase
{
    private readonly ReadOnlyObservableCollection<Room> _rooms;
    public RoomServiceValidator(ReadOnlyObservableCollection<Room> rooms) => _rooms = rooms;

    public bool CanAddRoom(Room newRoom)
    {
        if (!DefaultValidate(newRoom, typeof(RoomValidator)))
            return false;

        if (_rooms.All(x => x.Number != newRoom.Number))
        {
            ValidatorConfig.OnSuccess?.Invoke();
            return true;
        }

        HandleError("Комната уже существует!");
        return false;
    }

    public bool CanEditRoom(Room newRoom)
    {
        if (!DefaultValidate(newRoom, typeof(RoomValidator))) return false;
        ValidatorConfig.OnSuccess?.Invoke();
        return true;
    }

    public bool CanReserved(Reservation reservation, int roomTargetNumber)
    {
        if (!DefaultValidate(reservation, typeof(ReservationValidator))) return false;

        if (_rooms.Any(x => x.Reservation is not null && x.Number == roomTargetNumber))
        {
            HandleError("Комната уже забронирована!");
            return false;
        }

        ValidatorConfig.OnSuccess?.Invoke();
        return true;
    }
}