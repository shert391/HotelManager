using DevExpress.Mvvm;

namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

public enum DialogViewModelState
{
    Edit,
    Default,
}
public abstract class AbstractDialogViewModel : BindableBase
{
    public DialogViewModelState CurrentViewState { get; set; } = DialogViewModelState.Default;
    public AbstractDialogViewModel? Parent { get; set; }
}