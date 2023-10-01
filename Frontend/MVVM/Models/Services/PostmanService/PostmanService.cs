using DataContract.ViewModelsDto.Messages;

namespace HotelManager.MVVM.Models.Services.PostmanService;

public class PostmanService : IPostmanService
{
    public event Action<IMessage>? NewMessage;
    public void SendNewMessage(IMessage message) => NewMessage?.Invoke(message);
}