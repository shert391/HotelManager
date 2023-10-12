using System.Collections.ObjectModel;

namespace DataContract.BusinessModels;

public class Reservation
{
    public ObservableCollection<People> Peoples { get; init; } = new();
    
    public DateTime StartData { get; set; }
    public DateTime EndData { get; init; }

}
