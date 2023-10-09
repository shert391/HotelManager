using System.Collections.ObjectModel;
using DataContract.BusinessModels;

namespace DataContract.ViewModelsDto;
public class ApplicationDto
{
    public ObservableCollection<PeopleViewModel> Peoples { get; init; } = new();
    public DateTime EndData { get; init; }
    public RoomType Type { get; init; }
}