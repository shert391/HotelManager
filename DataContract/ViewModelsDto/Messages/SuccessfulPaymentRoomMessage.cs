using DevExpress.Mvvm;

namespace DataContract.ViewModelsDto.Messages;

/// <summary>
/// Сообщение отправляется, когда за комнату расчитались и бронь снята
/// </summary>
public class SuccessfulPaymentRoomMessage : BindableBase, IMessage
{
    public int RoomNumber;
}