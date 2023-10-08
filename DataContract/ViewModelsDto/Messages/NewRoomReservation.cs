using DevExpress.Mvvm;

namespace DataContract.ViewModelsDto.Messages;

public class NewRoomReservation : BindableBase, IMessage
{
    public int RoomNumber;
}