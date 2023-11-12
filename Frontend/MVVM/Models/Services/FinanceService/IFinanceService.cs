using System.Collections.ObjectModel;
using DataContract.BusinessModels;
using DataContract.DTO.MappingEntities;
using DataContract.DTO.Messages;
using DataContract.DTO.ViewModels;

namespace HotelManager.MVVM.Models.Services.FinanceService;

public interface IFinanceService
{
    /// <summary>
    /// <b>GetRoomTotalPrice</b> - вычисляет общую цену для оплаты.<i/><br/><br/>
    /// Результирующим значением является разница между концом действия брони и началом в днях умноженное на ежесуточную цену
    /// </summary>
    /// <returns></returns>
    public decimal GetRoomTotalPrice(Room room);
    
    /// <summary>
    /// <b>GetRoomFinePrice</b> - вычисляет сумму штрафа на основании имеющихся данных.<i/><br/><br/>
    /// Значение вычисляется так: если кол-во просроченных дней >= 1, то результатом будет стоимость просроченных дней + 30% от ежедневной стоимости комнаты 
    /// </summary>
    /// <returns></returns>
    public decimal GetRoomFinePrice(Room room);

    /// <summary>
    /// Возвращает общую выручку отеля
    /// </summary>
    /// <returns></returns>
    public decimal GetHotelRevenues();
    
    /// <summary>
    /// Оплатить комнату
    /// </summary>
    /// <param name="payInfo"></param>
    /// <returns></returns>
    public bool PayRoom(NeedPaymentMessage needPay);
    
    /// <summary>
    /// Для передачи ViewModel информации об истории оплат
    /// </summary>
    /// <returns></returns>
    public ObservableCollection<PayInformationViewModel> GetPayHistory();

    public double NumberDaysLived(Room room);

    public void ReturnBackup();
    public void CreateBackup();
}