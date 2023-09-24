using System.Windows.Input;
using DevExpress.Mvvm;

namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

public class DialogMessageBoxInformationViewModel : AbstractDialogMessageBoxViewModel
{
    public ICommand OkCommand { get; }

    public DialogMessageBoxInformationViewModel(string message, Action okMethod) : base(message) =>
        OkCommand = new DelegateCommand(okMethod);
    
}