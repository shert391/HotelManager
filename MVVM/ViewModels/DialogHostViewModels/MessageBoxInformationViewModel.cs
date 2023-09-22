using System.Windows.Input;
using DevExpress.Mvvm;

namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

public class MessageBoxInformationViewModel : AbstractMessageBoxViewModel
{
    public ICommand OkCommand { get; }

    public MessageBoxInformationViewModel(string message, Action okMethod) : base(message) =>
        OkCommand = new DelegateCommand(okMethod);
    
}