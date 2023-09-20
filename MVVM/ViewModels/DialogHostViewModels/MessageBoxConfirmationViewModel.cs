using DevExpress.Mvvm;
using HotelManager.MVVM.Utils;
using System.Windows.Input;

namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

public class MessageBoxConfirmationViewModel : AbstractMessageBoxViewModel
{
    public ICommand AcceptCommand { get; }
    public ICommand CancelCommand { get; }

    public MessageBoxConfirmationViewModel(Action onAgreement, string message) : base(message)
    {
        AcceptCommand = new DelegateCommand(() => {
            onAgreement();
            DialogHostController.Close();
        });

        CancelCommand = new DelegateCommand(DialogHostController.Close);
    }
}