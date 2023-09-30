using DataContract.BusinessModels;

namespace HotelManager.MVVM.Models.Services.RoomService;

public interface IRoomServiceValidator
{
    public bool CanEditRoom(Room newRoom);
    public bool CanAddRoom(Room newRoom);
}

