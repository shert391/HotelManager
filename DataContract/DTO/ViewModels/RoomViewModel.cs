using DataContract.BusinessModels;
using DataContract.DTO.Messages;
using DataContract.Extensions;
using DevExpress.Mvvm;

namespace DataContract.DTO.ViewModels;

public enum RoomState
{
    Busy,
    Free,
    NeedPaid,
}

public class RoomViewModel : BindableBase
{
    public int Number { get; set; }
    public decimal Score { get; set; }

    public decimal Price { get; set; }
    public RoomType Type { get; set; }
    public int MaxPeoples => Type.GetMaxPeople();
    public NeedPaymentMessage? NeedPayment { get; set; }
    public RoomState CurrentState { get; set; } = RoomState.Free;
    public RoomViewModel Clone() => (RoomViewModel)MemberwiseClone();
}