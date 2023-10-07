using DataContract.BusinessModels;
using DataContract.GlobalConstants;

namespace HotelManager.MVVM.Models.Services.DebugHelperService;

public class DebugHelperService : AbstractHotelService, IDebugHelperService
{
    private readonly Random _random = new();
    
    public void GenerateTestRooms()
    {
        for (var i = 1; i <= _random.Next(RoomConstants.MinRoomNumber, RoomConstants.MaxRoomNumber); i++)
        {
            var newRoom = new Room
            {
                Number = i,
                Price = _random.Next((int)RoomConstants.MinPrice, (int)RoomConstants.MaxPrice),
                Type = (RoomType)_random.Next(0, Enum.GetNames(typeof(RoomType)).Length)
            };
            if(GlobalLocalStorage.GetRoom(i) is null)
                Rooms.Add(newRoom);
        }
    }
}