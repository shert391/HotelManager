using System.Collections.ObjectModel;
using DataContract.ViewModelsDto;
using HotelManager.MVVM.Utils;

namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

public class PayHistoryViewModel : AbstractDialogViewModel, IConfigurable<ObservableCollection<PayInformationViewModel>>
{
    public ObservableCollection<PayInformationViewModel>? Payments { get; private set; }
    public decimal GlobalCurrency => Payments!.Sum(x => x.TotalPrice);
    public void Configure(ObservableCollection<PayInformationViewModel> payments) => Payments = payments;
}