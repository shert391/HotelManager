using System.Collections.ObjectModel;
using DataContract.BusinessModels;
using DataContract.ViewModelsDto.Messages;

namespace HotelManager.MVVM.Models.Services.FinanceService;
public class FinanceService : AbstractHotelManager, IFinanceService
{
    private readonly List<PayInformation> _payHistory = new();
    
    public decimal GetTotalPrice(Room room)
    {
        var delta = (room.Reservation!.EndData - room.Reservation!.StartData).TotalDays;
        var price = delta * (double)room.Price;
        return (decimal)price;
    }

    public decimal GetFinePrice(Room room)
    {
        var delta = (DateTime.Now - room.Reservation!.EndData).TotalDays;
        
        if (delta < 1)
            return 0;

        var fine = delta * (double)room.Price + (double)room.Price * 0.3;
        
        return (decimal)fine;
    }

    public bool Pay(PayInformationDto payInformationDto)
    {
        _payHistory.Add(Mapper.Map<PayInformation>(payInformationDto));
        var room = GlobalLocalStorage.GetRoom(payInformationDto.NumberRoom);
        room!.Reservation = null;
        return true;
    }

    public ObservableCollection<PayInformationDto> GetPayHistory()
    {
        return Mapper.Map<ObservableCollection<PayInformationDto>>(_payHistory);
    }
}

