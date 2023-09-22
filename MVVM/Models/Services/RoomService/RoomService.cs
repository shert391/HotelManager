using DevExpress.Mvvm.Native;
using HotelManager.MVVM.Models.DataContract;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;

namespace HotelManager.MVVM.Models.Services.RoomService;

public class RoomService : IRoomService
{
    private readonly IRoomServiceValidator _validator;
    private readonly ObservableCollection<Room> _rooms = new();
    private readonly ReadOnlyObservableCollection<Room> _roomsReadonly;

    public event Action? RoomCollectionChanged;

    public RoomService()
    {
        _roomsReadonly = new(_rooms);
        _validator = new RoomServiceValidator(_roomsReadonly);
    }

    public ReadOnlyObservableCollection<Room> GetRooms() => _roomsReadonly;

    private void ChangeCollection(Action changedMethod)
    {
        changedMethod();
        RoomCollectionChanged?.Invoke();
    }

    public void AddRoom(Room newRoom, RoomServiceValidatorConfig config)
    {
        _validator.Configurate(config);
        _validator.AddRoom(newRoom, () => ChangeCollection(() => _rooms.Add(newRoom)));
    }

    public ReadOnlyObservableCollection<Room> Find(int roomNumber) => new(_rooms.Where(room => room.Number == roomNumber).ToObservableCollection());

    public void DeleteRoom(int roomNumber) => ChangeCollection(() => { _rooms.Remove(_rooms.Single(room => room.Number == roomNumber)); });

    public void EditRoom(Room editableRoom, RoomServiceValidatorConfig config)
    {
        _validator.Configurate(config);
        _validator.EditRoom(
        editableRoom, 
        () => ChangeCollection(() =>
        {
            var index = _rooms.IndexOf(room => room.Number == editableRoom.Number);
            _rooms[index] = editableRoom.Clone();
        }));
    }
};
