namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

public abstract class AbstractDialogMessageBoxViewModel : AbstractDialogViewModel
{
    public string Message { get; }
    protected AbstractDialogMessageBoxViewModel(string message) => Message = message;
}

