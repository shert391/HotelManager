namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

public abstract class AbstractMessageBoxViewModel : BaseViewModel, IDialogViewModel
{
    public string Message { get; }

    protected AbstractMessageBoxViewModel(string message) => Message = message;
}

