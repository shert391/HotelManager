using System.Collections.ObjectModel;
using DataContract.BusinessModels;
using DataContract.DTO.MappingEntities;
using DataContract.DTO.Messages;
using DataContract.DTO.ViewModels;
using HotelManager.MVVM.Models.Services.PostmanService;

namespace HotelManager.MVVM.Models.Services.FinanceService;
public class FinanceService : AbstractHotelService, IFinanceService
{
    private readonly List<PayInformation> _payHistory = new();
    private readonly List<PayInformation> _payHistoryBackup = new();
    private readonly IPostmanService _postman;

    public FinanceService(IPostmanService postman) => _postman = postman;

    public double NumberDaysLived(Room room) => (room.Reservation!.EndData - room.Reservation!.StartData).TotalDays;
    
    public decimal GetRoomTotalPrice(Room room)
    {
        var delta = NumberDaysLived(room);
        var price = delta * (double)room.Price;
        return (decimal)price;
    }

    public decimal GetHotelRevenues() => _payHistory.Sum(payInfo => payInfo.TotalPrice);
    
    public decimal GetRoomFinePrice(Room room)
    {
        var delta = (DateTime.Now - room.Reservation!.EndData).TotalDays;
        
        if (delta < 1)
            return 0;

        var fine = delta * (double)room.Price + (double)room.Price * 0.3;
        
        return (decimal)fine;
    }

    public bool PayRoom(NeedPaymentMessage needPay)
    {
        var room = GlobalLocalStorage.GetRoom(needPay.NumberRoom);

        if (room is null)
            throw new Exception("Room is null?!");
        
        var payInfo = Mapper.Map<PayInformation>(needPay);
        payInfo.Type = room.Type;
        payInfo.PricePerDay = room.Price;

        _payHistory.Add(payInfo);
        room.Reservation = null;
        _postman.SendNewMessage(new SuccessfulPaymentRoomMessage { RoomNumber = payInfo.NumberRoom });
        
        return true;
    }

    public ObservableCollection<PayInformationViewModel> GetPayHistory()
    {
        return Mapper.Map<ObservableCollection<PayInformationViewModel>>(_payHistory);
    }

    public void ReturnBackup()
    {
        _payHistory.Clear();
        foreach (var infoBackup in _payHistoryBackup)
            _payHistory.Add(Mapper.Map<PayInformation>(infoBackup));
        _payHistoryBackup.Clear();
    }

    public void CreateBackup()
    {
        _payHistoryBackup.Clear();
        foreach (var info in _payHistory)
            _payHistoryBackup.Add(Mapper.Map<PayInformation>(info));
        _payHistory.Clear();
    }
}

