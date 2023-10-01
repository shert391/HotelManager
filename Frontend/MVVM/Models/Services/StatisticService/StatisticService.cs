using DataContract.BusinessModels;

namespace HotelManager.MVVM.Models.Services.StatisticService;

public class StatisticService : AbstractHotelManager, IStatisticService
{
    public decimal GetScore(int roomNumber)
    {
        var room = GlobalLocalStorage.GetRoom(roomNumber);
        if (room!.Feedbacks.Count == 0) return 0;
        return (decimal)room.Feedbacks.Sum(x => x.Score) / room.Feedbacks.Count;
    }

    public void SendFeedback(int roomNumber, double numberStars)
    {
        var room = GlobalLocalStorage.GetRoom(roomNumber);
        room!.Feedbacks.Add(new Assessment
        {
            SendDate = DateTime.Now,
            RoomNumber = roomNumber,
            Score = numberStars,
        });
    }
}

