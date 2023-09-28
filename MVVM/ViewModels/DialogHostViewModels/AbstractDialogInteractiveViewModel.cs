namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

public abstract class AbstractDialogInteractiveViewModel : AbstractAppManagerViewModel, IDialogViewModel
{
    public IDialogViewModel? Parent { get; set; }
}