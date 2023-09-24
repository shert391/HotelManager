using HotelManager.InitApp;
using MaterialDesignThemes.Wpf;
using HotelManager.MVVM.ViewModels.DialogHostViewModels;

namespace HotelManager.MVVM.Utils;

public static class DialogHostController
{
    private const string _dialogHostName = "MainDialogHost";
    private static IDialogViewModel? _currentDialogViewModel;
    
    public static void BackViewModel()
    {
        var dialogSession = DialogHost.GetDialogSession(_dialogHostName);
        if (dialogSession is null) return;
        _currentDialogViewModel = _currentDialogViewModel?.Parent;
        dialogSession.UpdateContent(_currentDialogViewModel);
    }
    
    private static void ShowViewModel(IDialogViewModel newViewModel)
    {
        var dialogSession = DialogHost.GetDialogSession(_dialogHostName);

        if (dialogSession is null)
        {
            DialogHost.Show(newViewModel, _dialogHostName);
            return;
        }

        newViewModel.Parent = dialogSession.Content as IDialogViewModel;
        _currentDialogViewModel = newViewModel;
        
        dialogSession.UpdateContent(newViewModel);
    }

    public static void Show<T>() where T : notnull, IDialogViewModel
    {
        var dialogViewModel = App.Resolve<T>();
        ShowViewModel(dialogViewModel);
    }

    public static void Show<T, TOption>(TOption option) where T : notnull, IDialogViewModel, IConfigurable<TOption>
    {
        var dialogViewModel = App.Resolve<T, TOption>(option);
        ShowViewModel(dialogViewModel);
    }

    public static void ShowMessageBoxInformation(string message, bool isClose = false)
    {
        DialogMessageBoxInformationViewModel messageBoxViewModel;
        if(isClose) messageBoxViewModel = new(message, DialogHostController.Close);
        else messageBoxViewModel = new(message, DialogHostController.BackViewModel);
        ShowViewModel(messageBoxViewModel);
    }
    
    public static void ShowMessageBoxConfirmation(Action onAgreement , string message)
    {
        var messageBoxViewModel = new DialogMessageBoxConfirmationViewModel(onAgreement, message);
        ShowViewModel(messageBoxViewModel);
    }

    public static void Close() => DialogHost.Close(_dialogHostName);
}