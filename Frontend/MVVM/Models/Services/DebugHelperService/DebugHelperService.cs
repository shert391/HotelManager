using DataContract.BusinessModels;
using DataContract.GlobalConstants;
using DataContract.ViewModelsDto;

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

    public void AddHoursToStorageTime(int countHours) => GlobalLocalStorage.AddHoursForTest += countHours;
    
    public IEnumerable<ApplicationDto> GenerateApplications(int countApplication)
    {
        var result = new List<ApplicationDto>();
        
        for (var i = 0; i < countApplication; i++)
        {
            result.Add(new ApplicationDto
            {
                Peoples = _random.Next(1, 4),
                EndData = DateTime.Now.AddHours(_random.Next(1, 3)),
                Type = (RoomType)_random.Next(0, Enum.GetNames(typeof(RoomType)).Length - 1),
            });
        }

        return result;
    }
}