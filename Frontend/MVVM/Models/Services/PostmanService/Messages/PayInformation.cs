namespace HotelManager.MVVM.Models.Services.PostmanService.Messages;

public class PayInformation : IMessage
{
    public int NumberRoom { get; init; }
    public decimal Price { get; init; }
}