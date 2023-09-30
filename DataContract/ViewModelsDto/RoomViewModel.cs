using DevExpress.Mvvm;
using DataContract.BusinessModels;
using DataContract.Extensions;

namespace DataContract.ViewModelsDto;

public enum RoomState
{
    Busy,
    Free,
    NeedPaid,
}

public class RoomViewModel : BindableBase
{
    public int Number { get; set; }
    public decimal Price { get; set; }
    public RoomType Type { get; set; }
    public decimal NeedPaid { get; set; }

    public RoomState CurrentState { get; set; } = RoomState.Free;
    public int MaxPeoples => Type.GetMaxPeople();
    
    public RoomViewModel Clone() => (RoomViewModel)MemberwiseClone();
}