using System.Collections.ObjectModel;

namespace HotelManager.MVVM.Models.DataContract;
public class Reservation
{
    public ObservableCollection<People>? Peoples { get; init; }
    public DateTime EndData { get; init; }
    
    public Reservation Clone() => (Reservation)MemberwiseClone();
}
