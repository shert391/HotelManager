using DataContract.DTO.ViewModels;
using HotelManager.MVVM.Utils;
using System.Collections.ObjectModel;

namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

public class NoHandledApplicationViewModel : AbstractDialogViewModel, IConfigurable<ObservableCollection<ApplicationViewModel>>
{
    public ObservableCollection<ApplicationViewModel> NoHandledApplication { get; private set; } = new();

    public void Configure(ObservableCollection<ApplicationViewModel> collectionLink) => NoHandledApplication = collectionLink;
}

