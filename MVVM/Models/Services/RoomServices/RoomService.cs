using DevExpress.Mvvm.Native;
using HotelManager.MVVM.Models.DataContract;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using HotelManager.MVVM.Models.Configs;
using HotelManager.MVVM.Models.DataContract.Requests;

namespace HotelManager.MVVM.Models.Services.RoomServices;

public class RoomService : IRoomService
{
    private readonly RoomServiceValidator _roomServiceValidator;
    private readonly ObservableCollection<Room> _rooms = new();
    private readonly ReadOnlyObservableCollection<Room> _roomsReadonly;

    public RoomService()
    {
        _roomsReadonly = new(_rooms);
        _roomServiceValidator = new RoomServiceValidator(_roomsReadonly);
    }

    public ReadOnlyObservableCollection<Room> GetRooms() => _roomsReadonly;

    public void SubscribeChange(NotifyCollectionChangedEventHandler subscriber) =>
        _rooms.CollectionChanged += subscriber;

    public ReadOnlyObservableCollection<Room> Find(int roomNumber) =>
        new(_rooms.Where(room => room.Number == roomNumber).ToObservableCollection());

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
        if (!_roomServiceValidator.CanReserved(reservation)) return;

        var index = _rooms.IndexOf(room => room.Number == targetRoomNumber);
        _rooms[index].Reservation = reservation.Clone();
    }

    public void EditRoom(RoomChangeRequest roomChangeRequest, IDefaultValidatorConfig validatorConfig)
    {
        var index = _rooms.IndexOf(room => room.Number == roomChangeRequest.NumberTargetRoom);
        
        var newRoom = new Room()
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