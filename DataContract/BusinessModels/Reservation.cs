using System.Collections.ObjectModel;

namespace DataContract.BusinessModels;

public class Reservation
{
    public ObservableCollection<People> Peoples { get; init; }
    
    public DateTime StartData { get; } = DateTime.Now;
    public DateTime EndData { get; init; }

}
