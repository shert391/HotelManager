using System.Collections.ObjectModel;

namespace HotelManager.MVVM.Models.DataContract;

public interface IReservation {
    public DateTime EndData { get; }
}

public class Reservation : IReservation
{
    public ObservableCollection<People> Peoples { get; init; } = new();
    public DateTime EndData { get; init; }
    
    public Reservation Clone() => (Reservation)MemberwiseClone();
}
