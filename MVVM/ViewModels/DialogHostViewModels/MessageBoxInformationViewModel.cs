using System.Windows.Input;
using DevExpress.Mvvm;
using HotelManager.MVVM.Utils;

namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

public class MessageBoxInformationViewModel : AbstractMessageBoxViewModel
{
    public ICommand OkCommand { get; }

    public MessageBoxInformationViewModel(string message) : base(message) =>
        OkCommand = new DelegateCommand(DialogHostController.ShowPreview);
    
}