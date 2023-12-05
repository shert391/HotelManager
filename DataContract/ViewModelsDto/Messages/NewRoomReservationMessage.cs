using DevExpress.Mvvm;

namespace DataContract.ViewModelsDto.Messages;

/// <summary>
/// Это сообщение отправляется от сервисов вью-моделям, когда произошло новое бронирование комнаты
/// </summary>
public class NewRoomReservationMessage : BindableBase, IMessage
{
    public int RoomNumber;
}