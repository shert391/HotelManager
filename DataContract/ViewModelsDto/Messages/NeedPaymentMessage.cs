using DevExpress.Mvvm;

namespace DataContract.ViewModelsDto.Messages;

/// <summary>
/// Сообщение о платежной информации, которую должны осуществить, выселяющиеся жильцы
/// </summary>
public class NeedPaymentMessage : BindableBase, IMessage
{
    public int NumberRoom { get; init; }

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
    public decimal? AdditionalFines { get; set; }
    
    public decimal TotalPrice => Fines + Price + AdditionalFines.GetValueOrDefault();
}