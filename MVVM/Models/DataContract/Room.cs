using DevExpress.Mvvm;
using System.ComponentModel;
using HotelManager.MVVM.Models.CustomAttributes;
using HotelManager.MVVM.Models.Extensions;

namespace HotelManager.MVVM.Models.DataContract;

public enum RoomType
{
    [Description("Люкс"), MaxPeople(4)]
    Luxury,

    [Description("Двухместный"), MaxPeople(2)]
    Double,

    [Description("Одноместный"), MaxPeople(1)]
    Single,

    [Description("Полулюкс"), MaxPeople(3)]
    SemiLuxury,

    [Description("Двухместный с раскладным диваном"), MaxPeople(2)]
    DoubleConvertibleSofa,
}

public class Room
{
    public int Number { get; init; }

    public decimal Price { get; init; }

    public RoomType Type { get; init; }
    
    public Reservation? Reservation { get; set; }
    
    public Room Clone() => (Room)MemberwiseClone();
    
    public int MaxPeoples => Type.GetMaxPeople();
}