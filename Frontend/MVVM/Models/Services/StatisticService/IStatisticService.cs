namespace HotelManager.MVVM.Models.Services.StatisticService;

public interface IStatisticService
{
    public void SendFeedback(int roomNumber, double numberStars);
    public decimal GetScore(int roomNumber);
}
