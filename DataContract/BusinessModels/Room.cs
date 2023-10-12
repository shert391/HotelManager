using DataContract.CustomAttributes;
using System.ComponentModel;
using DataContract.GlobalConstants;

namespace DataContract.BusinessModels;

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

    public decimal Price { get; set; } = RoomConstants.MinPrice;

    public RoomType Type { get; init; } = RoomType.Single;
    public Reservation? Reservation { get; set; }
    
    public List<Assessment> Feedbacks = new(); 
}
