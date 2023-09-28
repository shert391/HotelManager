using DevExpress.Mvvm.Native;
using HotelManager.MVVM.Models.Configs;
using HotelManager.MVVM.Models.DataContract;
using HotelManager.MVVM.Models.DataContract.Requests;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace HotelManager.MVVM.Models.Services.RoomServices;

public class RoomService : IRoomService
{
    private readonly ObservableCollection<Room> _rooms = new();
    private readonly RoomServiceValidator _roomServiceValidator;

    public RoomService() => _roomServiceValidator = new RoomServiceValidator(_rooms);

    public IReadOnlyCollection<IRoom> GetRooms() => _rooms;
    public IReadOnlyCollection<IPeople> GetPeoplesInfo(int targetRoomIndex) => GetReservation(FindRoom(targetRoomIndex)).Peoples;

    private Reservation GetReservation(Room room)
    {
        if (room.Reservation is null)
            throw new Exception("Reservation is null");

        return room.Reservation;
    }
    private Room FindRoom(int numberRoom) => _rooms.First(room => room.Number == numberRoom);
    
    public T GetReservationInfo<T>(int configTargetRoomIndex, Func<IReservation, T> expression)
    {
        var reservation = FindRoom(configTargetRoomIndex);
        return expression(GetReservation(reservation));
    }

    public void SubscribeChange(NotifyCollectionChangedEventHandler subscriber) =>
        _rooms.CollectionChanged += subscriber;

    public void DeleteRoom(int roomNumber) => _rooms.Remove(_rooms.Single(room => room.Number == roomNumber));

    public void AddRoom(Room newRoom, IDefaultValidatorConfig validatorValidatorConfig)
    {
        _roomServiceValidator.Configurate(validatorValidatorConfig);
        if (_roomServiceValidator.CanAddRoom(newRoom))
            _rooms.Add(newRoom);
    }

    public void ReservedRoom(Reservation reservation, IDefaultValidatorConfig validatorConfig, int targetRoomNumber)
    {
        _roomServiceValidator.Configurate(validatorConfig);
        if (!_roomServiceValidator.CanReserved(reservation, targetRoomNumber)) return;

        var index = _rooms.IndexOf(room => room.Number == targetRoomNumber);
        _rooms[index].Reservation = reservation.Clone();
    }

    public void EditRoom(RoomChangeRequest roomChangeRequest, IDefaultValidatorConfig validatorConfig)
    {
        var index = _rooms.IndexOf(room => room.Number == roomChangeRequest.NumberTargetRoom);

        var newRoom = new Room
        {
            Number = roomChangeRequest.NumberTargetRoom,
            Price = roomChangeRequest.NewPrice,
            Type = roomChangeRequest.NewType,

            Reservation = _rooms[index].Reservation,
        };

        _roomServiceValidator.Configurate(validatorConfig);
        if (!_roomServiceValidator.CanEditRoom(newRoom)) return;

        _rooms[index] = newRoom;
    }
};