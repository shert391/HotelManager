using System.Collections.ObjectModel;

namespace HotelManager.MVVM.Models.DataContract;
public class Reservation
{
    public ObservableCollection<People> Peoples { get; }
    public DateTime EndData { get; }
    
    public Reservation Clone() => (Reservation)MemberwiseClone();
}
