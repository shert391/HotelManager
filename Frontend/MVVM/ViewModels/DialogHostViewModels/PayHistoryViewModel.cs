using System.Collections.ObjectModel;
using DataContract.ViewModelsDto.Messages;
using HotelManager.MVVM.Utils;

namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

public class PayHistoryViewModel : AbstractDialogViewModel, IConfigurable<ObservableCollection<PayInformationDto>>
{
    public ObservableCollection<PayInformationDto>? Payments { get; private set; }
    public decimal GlobalCurrency => Payments!.Sum(x => x.TotalPrice);
    public void Configure(ObservableCollection<PayInformationDto> payments) => Payments = payments;
}