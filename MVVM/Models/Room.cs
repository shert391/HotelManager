namespace HotelManager.MVVM.Models;

public enum RoomType
{
    Luxury,
    Double,
    Single,
    SemiLuxury,
    DoubleConvertibleSofa
}

public class Room
{
    public int Number { get; set; }
    public string Type { get; set; }
    public decimal Price { get; set; }

}

