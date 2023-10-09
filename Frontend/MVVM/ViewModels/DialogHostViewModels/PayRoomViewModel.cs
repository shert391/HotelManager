using DevExpress.Mvvm;
using HotelManager.MVVM.Models.Services.FinanceService;
using HotelManager.MVVM.Models.Services.StatisticService;
using HotelManager.MVVM.Utils;
using System.Windows.Input;
using DataContract.ViewModelsDto.Messages;

namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;
public class PayRoomViewModel : AbstractDialogViewModel, IConfigurable<NeedPaymentMessage>
{
    public ICommand PayCommand { get; }

    public IFinanceService FinanceService { get; set; }
    public IStatisticService StatisticService { get; set; }

    public NeedPaymentMessage NeedPayment { get; set; } = new();
    public double NumberStars { get; set; } = 5;

    public PayRoomViewModel(IFinanceService financeService, IStatisticService statisticService)
    {
        FinanceService = financeService;
        StatisticService = statisticService;
        PayCommand = new DelegateCommand(Pay);
    }

    private void Pay()
    {
        if(FinanceService.PayRoom(NeedPayment))
            StatisticService.SendFeedback(NeedPayment.NumberRoom, NumberStars);
        
        DialogHostController.Close();
    }

    public void Configure(NeedPaymentMessage needPayment) => NeedPayment = needPayment;
}

