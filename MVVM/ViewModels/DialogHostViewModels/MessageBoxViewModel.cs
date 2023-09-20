using DevExpress.Mvvm;
using HotelManager.MVVM.Utils;
using System.Windows.Input;

namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

internal class MessageBoxViewModel : BaseViewModel, IDialogViewModel
{
    public string Message { get; }
    public ICommand AcceptCommand { get; }

    public MessageBoxViewModel(string message)
    {
        Message = message;
        AcceptCommand = new DelegateCommand(DialogHostController.ShowPreview);
    }
}

