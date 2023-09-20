namespace HotelManager.MVVM.Models.Services;

public interface ITestService
{
    public void GenerateTestRooms(
        int minRooms,
        int maxRooms,
        int minPrice,
        int maxPrice);
}