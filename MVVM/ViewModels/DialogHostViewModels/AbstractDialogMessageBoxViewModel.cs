namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

public abstract class AbstractDialogMessageBoxViewModel : IDialogViewModel
{
    public string Message { get; }

    public IDialogViewModel? Parent { get; set; }
    protected AbstractDialogMessageBoxViewModel(string message) => Message = message;
}

