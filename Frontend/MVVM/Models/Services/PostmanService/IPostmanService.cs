using DataContract.DTO.Messages;

namespace HotelManager.MVVM.Models.Services.PostmanService;

public interface IPostmanService
{
    public event Action<IMessage>? NewMessage;

    public void SendNewMessage(IMessage information);
}