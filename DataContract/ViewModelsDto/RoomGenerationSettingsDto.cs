using DevExpress.Mvvm;
using DataContract.BusinessModels;

namespace DataContract.ViewModelsDto;
public class RoomGenerationSettingsDto : BindableBase
{
    public int MinRooms { get; set; } = 20;
    public int MaxRooms { get; set; } = 30;

    /// <summary>
    /// ВАЖНО!
    /// Названия свойств стоимости комнат должны содержать подстроки названия соответствующего <see cref="RoomType"/> значения - это необходимо
    /// для доставания содержимого из поля в <see cref="RoomGenerationSettingsDto"/> по его названию через рефлексию, чтобы избежать "спагетти" их условных операторов при проецировании цены на тип комнаты
    /// </summary>
    public decimal SinglePrice { get; set; } = 1000;
    public decimal DoublePrice { get; set; } = 3000;
    public decimal LuxuryPrice { get; set; } = 10000;
    public decimal SemiLuxuryPrice { get; set; } = 6000;
    public decimal DoubleConvertibleSofaPrice { get; set; } = 4000;
}
