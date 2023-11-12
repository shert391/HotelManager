using System.Collections.ObjectModel;
using DevExpress.Mvvm;

namespace DataContract.DTO.ViewModels;

public class ReservationViewModel : BindableBase
{
    public ObservableCollection<PeopleViewModel> Peoples { get; init; } = new();
    public DateTime EndData { get; init; }
}
