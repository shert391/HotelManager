using System.Collections.ObjectModel;
using DataContract.BusinessModels;
using DataContract.ViewModelsDto.Messages;

namespace HotelManager.MVVM.Models.Services.FinanceService;

public interface IFinanceService
{
    /// <summary>
    /// <b>GetTotalPrice</b> - вычисляет общую цену для оплаты.<i/><br/><br/>
    /// Результирующим значением является разница между концом действия брони и началом в днях умноженное на ежесуточную цену
    /// </summary>
    /// <returns></returns>
    public decimal GetTotalPrice(Room room);


    /// <summary>
    /// <b>GetFinePrice</b> - вычисляет сумму штрафа на основании имеющихся данных.<i/><br/><br/>
    /// Значение вычисляется так: если кол-во просроченных дней >= 1, то результатом будет стоимость просроченных дней + 30% от ежедневной стоимости комнаты 
    /// </summary>
    /// <returns></returns>
    public decimal GetFinePrice(Room room);

    public bool Pay(PayInformationDto payInformationDto);
    
    public ObservableCollection<PayInformationDto> GetPayHistory();
}