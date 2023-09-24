namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

public abstract class AbstractDialogManagerViewModel : AbstractAppManagerViewModel, IDialogViewModel
{
    public IDialogViewModel? Parent { get; set; }
}