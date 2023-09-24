namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

public interface IDialogViewModel
{
    public IDialogViewModel? Parent { get; set; }
}