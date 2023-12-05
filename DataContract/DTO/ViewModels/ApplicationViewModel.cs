using System.Collections.ObjectModel;
using DataContract.BusinessModels;

namespace DataContract.DTO.ViewModels;

public class ApplicationViewModel
{
    public ObservableCollection<PeopleViewModel> Peoples { get; init; } = new();
    public DateTime EndData { get; init; }
    public RoomType Type { get; init; }
}