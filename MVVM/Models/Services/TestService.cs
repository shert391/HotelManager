using HotelManager.MVVM.Models.DataContract;

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
        if (maxRooms > Room.MaxNumber)
            throw new ArgumentException("maxRooms > Room.MaxNumber what?!");
        
        if (maxRooms < minRooms)
            throw new ArgumentException("maxRooms < minRooms what?!");
        
        if (minPrice < Room.MinPrice)
            throw new ArgumentException("minPrice < Room.MinPrice what?!");
        
        if (maxPrice > Room.MaxPrice)
            throw new ArgumentException("maxPrice > Room.MaxPrice what?!");

        for (var i = Room.MinNumber; 
             i <= _random.Next(minRooms, maxRooms); 
             i++)
        {
            _roomService.AddRoom(new Room()
            {
                Number = i,
                Price = _random.Next(minPrice, maxPrice),
                Type = (RoomType)_random.Next(0, Enum.GetNames(typeof(RoomType)).Length)
            }, false);
        }
    }
}