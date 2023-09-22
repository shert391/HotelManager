using HotelManager.MVVM.Models.Builders;
using HotelManager.MVVM.Models.DataContract;
using System.Collections.ObjectModel;

namespace HotelManager.MVVM.Models.Services.RoomService;

public class RoomServiceValidator : IRoomServiceValidator
{
    private readonly RoomValidator _roomValidator = new();
    private readonly ReadOnlyObservableCollection<Room> _rooms;
    private IRoomServiceValidatorConfig _config = RoomServiceValidatorConfigBuilder.CreateDefault().Build();

    public RoomServiceValidator(ReadOnlyObservableCollection<Room> rooms) => _rooms = rooms;

    public void Configurate(IRoomServiceValidatorConfig config) => _config = config;

    private bool ValidateRoom(Room room)
    {
        var status = _roomValidator.Validate(room);
        if (status.IsValid) return status.IsValid;
        var error = status.Errors.First().ErrorMessage;
        _config.OnError?.Invoke(error);
        if (_config.IsGenerateException) throw new ArgumentException(error);
        return status.IsValid;
    }
    private void DefaultValidateOnUpdateCollection(Room newRoom, Func<bool> additionalVerification)
    {
        if (!ValidateRoom(newRoom))
            return;

        if (additionalVerification())
            _config.OnSuccess?.Invoke();
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
            _config.OnError?.Invoke(error);
            if (_config.IsGenerateException) throw new ArgumentException(error);
            return false;
        });
    }

    public void EditRoom(Room newRoom, Action editMethod)
    {
        DefaultValidateOnUpdateCollection(newRoom, () =>
        {
            editMethod();
            return true;
        });
    }
}
