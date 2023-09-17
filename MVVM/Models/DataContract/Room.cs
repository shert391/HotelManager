using System.ComponentModel;

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
    DoubleConvertibleSofa
}

public class Room
{
    public int Number { get; set; }
    public RoomType Type { get; set; }
    public decimal Price { get; set; }
}
