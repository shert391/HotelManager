using System.Collections.ObjectModel;
using DataContract.BusinessModels;
using DataContract.ViewModelsDto.Messages;
using HotelManager.MVVM.Models.Services.PostmanService;

namespace HotelManager.MVVM.Models.Services.FinanceService;
public class FinanceService : AbstractHotelService, IFinanceService
{
    private readonly List<PayInformation> _payHistory = new();
    private readonly List<PayInformation> _payHistoryBackup = new();
    private readonly IPostmanService _postman;

    public FinanceService(IPostmanService postman) => _postman = postman;

    public decimal GetRoomTotalPrice(Room room)
    {
        var delta = (room.Reservation!.EndData - room.Reservation!.StartData).TotalDays;
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

    public bool PayRoom(NeedPaymentMessage needPaymentMessage)
    {
        _payHistory.Add(Mapper.Map<PayInformation>(needPaymentMessage));
        var room = GlobalLocalStorage.GetRoom(needPaymentMessage.NumberRoom);
        room!.Reservation = null;
        _postman.SendNewMessage(new SuccessfulPaymentRoomMessage()
        {
            RoomNumber = needPaymentMessage.NumberRoom
        });
        return true;
    }

    public ObservableCollection<NeedPaymentMessage> GetPayHistory()
    {
        return Mapper.Map<ObservableCollection<NeedPaymentMessage>>(_payHistory);
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

