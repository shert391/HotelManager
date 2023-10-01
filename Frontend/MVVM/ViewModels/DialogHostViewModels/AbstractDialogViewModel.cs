using System.Windows.Input;
using DevExpress.Mvvm;
using HotelManager.MVVM.Utils;

namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

public enum DialogViewModelState
{
    Edit,
    Default,
}
public abstract class AbstractDialogViewModel : BindableBase
{
    public ICommand CancelCommand { get; protected set; } = new DelegateCommand(DialogHostController.Close);
    public DialogViewModelState CurrentViewState { get; set; } = DialogViewModelState.Default;
    public AbstractDialogViewModel? Parent { get; set; }
}