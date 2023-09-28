using HotelManager.MVVM.Models.Builders;
using HotelManager.MVVM.Models.DataContract;
using HotelManager.MVVM.Models.Services.RoomServices;

namespace HotelManager.MVVM.Models.Services;

public class TestService : ITestService
{
    private readonly IRoomService _roomService;
    private readonly Random _random = new();

    public TestService(IRoomService roomService) => _roomService = roomService;
    
    public void GenerateTestRooms(
        int minRooms, 
        int maxRooms, 
        int minPrice, 
        int maxPrice)
    {
        for (var i = 1; 
             i <= _random.Next(minRooms, maxRooms); 
             i++)
        {
            _roomService.AddRoom(new Room()
            {
                Number = i,
                Price = _random.Next(minPrice, maxPrice),
                Type = (RoomType)_random.Next(0, Enum.GetNames(typeof(RoomType)).Length)
            }, DefaultValidatorConfigBuilder.Create().Build());
        }
  }
}