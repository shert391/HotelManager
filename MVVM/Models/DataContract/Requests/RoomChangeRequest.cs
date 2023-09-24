using DevExpress.Mvvm;

namespace HotelManager.MVVM.Models.DataContract.Requests;
public class RoomChangeRequest : BindableBase
{
    public int NumberTargetRoom { get; set; }

    public decimal NewPrice { get; set; }

    public RoomType NewType { get; set; }
}

