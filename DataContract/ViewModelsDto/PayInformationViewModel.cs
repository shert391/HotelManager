using DataContract.BusinessModels;
using DevExpress.Mvvm;

namespace DataContract.ViewModelsDto;

public class PayInformationViewModel : BindableBase
{
    public int NumberRoom { get; init; }

    public string? RoomType { get; init; }

    public decimal PricePerDay { get; set; }

    /// <summary>
    /// Значение, показывающее сколько дней жильцами прожито в номере.
    /// Возможно не целое значение, в случае неполного дня.
    /// </summary>
    public double Lived { get; set; }
    
    /// <summary>
    /// Чистая стоимость за прожитые дни
    /// </summary>
    public decimal Price { get; init; }

    /// <summary>
    /// Штраф наложенный FinanceService, например за просрочку брони 
    /// </summary>
    public decimal Fines { get; init; }

    /// <summary>
    /// Дополнительный штраф выдвинутый менеджером отеля после 'условной' проверки номера, например за испорченную мебель....
    /// </summary>
    public decimal AdditionalFines { get; set; }
    
    public decimal TotalPrice => Fines + Price + AdditionalFines;
    
    public DateTime DatePayment { get; init; }
}