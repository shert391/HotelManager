using System.Collections.ObjectModel;
using DevExpress.Mvvm;

namespace DataContract.ViewModelsDto;

public class ReservationViewModel : BindableBase
{
    public ObservableCollection<PeopleViewModel> Peoples { get; init; }
    public DateTime EndData { get; init; }
}
