using DevExpress.Mvvm.Native;
using HotelManager.MVVM.Models.Builders;
using HotelManager.MVVM.Models.DataContract;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace HotelManager.MVVM.Models.Services.RoomService;

public class RoomService : IRoomService
{
    private readonly IRoomServiceValidator _validator;
    private readonly ObservableCollection<Room> _rooms = new();
    private readonly ReadOnlyObservableCollection<Room> _roomsReadonly;

    public RoomService()
    {
        _roomsReadonly = new(_rooms);
        _validator = new RoomServiceValidator(_roomsReadonly);
    }

    public ReadOnlyObservableCollection<Room> GetRooms() => _roomsReadonly;

    public void SubscribeChange(NotifyCollectionChangedEventHandler subscriber) => _rooms.CollectionChanged += subscriber;

    public void AddRoom(Room newRoom, IRoomServiceValidatorConfig config)
    {
        _validator.Configurate(config);
        _validator.AddRoom(newRoom, () => _rooms.Add(newRoom));
    }

    public ReadOnlyObservableCollection<Room> Find(int roomNumber) => new(_rooms.Where(room => room.Number == roomNumber).ToObservableCollection());

    public void DeleteRoom(int roomNumber) => _rooms.Remove(_rooms.Single(room => room.Number == roomNumber));

    public void EditRoom(Room editableRoom, IRoomServiceValidatorConfig config)
    {
        _validator.Configurate(config);
        _validator.EditRoom(
        editableRoom,
        () =>
        {
            var index = _rooms.IndexOf(room => room.Number == editableRoom.Number);
            _rooms[index] = editableRoom.Clone();
        });
    }
};
