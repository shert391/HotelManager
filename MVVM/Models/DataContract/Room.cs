using System.ComponentModel;
using HotelManager.InitApp;

namespace HotelManager.MVVM.Models.DataContract;

public enum RoomType
{
    [Description("Люкс")]
    Luxury,

    [Description("Двухместный")]
    Double,

    [Description("Одноместный")]
    Single,

    [Description("Полулюкс")]
    SemiLuxury,

    [Description("Двухместный с раскладным диваном")]
    DoubleConvertibleSofa,
}

public class Room
{
    #region Number
    public int Number { get; set; }
    public static readonly int MaxNumber = App.GetRoomSetting<int>(nameof(MaxNumber));
    public static readonly int MinNumber = 1;
    #endregion
    #region Price
    public decimal Price { get; set; }
    public static readonly decimal MaxPrice = App.GetRoomSetting<decimal>(nameof(MaxPrice));
    public static readonly decimal MinPrice = App.GetRoomSetting<decimal>(nameof(MinPrice));
    #endregion
    public RoomType Type { get; set; }
}
