using HotelManager.MVVM.Models.DataContract;
using HotelManager.MVVM.Utils;
using System.Collections.ObjectModel;

namespace HotelManager.MVVM.Models.Services.RoomService;

public class RoomServiceValidatorConfig : IConfiguration
{
    public Action? ActionOnSuccess { get; set; }
    public bool IsGenerateException { get; set; }
    public Action<string>? ActionOnError { get; set; }
}

public class RoomServiceValidator : IRoomServiceValidator
{
    private RoomServiceValidatorConfig _config = new();
    
    private readonly RoomValidator _roomValidator = new();
    private readonly ReadOnlyObservableCollection<Room> _rooms;

    public RoomServiceValidator(ReadOnlyObservableCollection<Room> rooms) => _rooms = rooms;


    public void Configurate(RoomServiceValidatorConfig config) => _config = config;

    private bool ValidateRoom(Room room)
    {
        var status = _roomValidator.Validate(room);
        if (status.IsValid) return status.IsValid;
        var error = status.Errors.First().ErrorMessage;
        _config.ActionOnError?.Invoke(error);
        if (_config.IsGenerateException) throw new ArgumentException(error);
        return status.IsValid;
    }
    private void DefaultValidateOnUpdateCollection(Room newRoom, Func<bool> additionalVerification)
    {
        if (!ValidateRoom(newRoom))
            return;

        if (additionalVerification())
            _config.ActionOnSuccess?.Invoke();
    }

    public void AddRoom(Room addRoom, Action addMethod)
    {
        DefaultValidateOnUpdateCollection(addRoom, () =>
        {
            if (!_rooms.Any(x => x.Number == addRoom.Number))
            {
                addMethod();
                return true;
            }
            var error = "Комната уже существует!";
            _config.ActionOnError?.Invoke(error);
            if (_config.IsGenerateException) throw new ArgumentException(error);
            return false;
        });
    }

    public void EditRoom(Room editableRoom, Action editMethod)
    {
        DefaultValidateOnUpdateCollection(editableRoom, () =>
        {
            editMethod();
            return true;
        });
    }
}
