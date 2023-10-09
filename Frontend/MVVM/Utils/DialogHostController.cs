using HotelManager.InitApp;
using MaterialDesignThemes.Wpf;
using HotelManager.MVVM.ViewModels.DialogHostViewModels;

namespace HotelManager.MVVM.Utils;

public static class DialogHostController
{
    private const string _dialogHostName = "MainDialogHost";
    private static AbstractDialogViewModel? _currentDialogViewModel;
    
    public static void BackViewModel(int count = 1)
    {
        var dialogSession = DialogHost.GetDialogSession(_dialogHostName);
        if (dialogSession is null) return;
        for (var i = 0; i < count; i++)
        {
            var parentViewModel = _currentDialogViewModel?.Parent;
            _currentDialogViewModel = parentViewModel;
            
            if (parentViewModel is null)
            {
                Close();
                return;
            }
            
            dialogSession.UpdateContent(_currentDialogViewModel);
        }
    }
    
    private static void ShowViewModel(AbstractDialogViewModel newViewModel)
    {
        var dialogSession = DialogHost.GetDialogSession(_dialogHostName);
        _currentDialogViewModel = newViewModel;

        if (dialogSession is null)
        {
            DialogHost.Show(newViewModel, _dialogHostName);
            return;
        }
        
        newViewModel.Parent = dialogSession.Content as AbstractDialogViewModel;
        dialogSession.UpdateContent(newViewModel);
    }

    public static void Show<T>() where T : notnull, AbstractDialogViewModel
    {
        var dialogViewModel = App.Resolve<T>();
        ShowViewModel(dialogViewModel);
    }
    
    public static void ConcreteShow(AbstractDialogViewModel viewModel) => ShowViewModel(viewModel);
    
    public static void Show<T, TOption>(TOption option) where T : notnull, AbstractDialogViewModel, IConfigurable<TOption>
    {
        var dialogViewModel = App.Resolve<T, TOption>(option);
        ShowViewModel(dialogViewModel);
    }

    public static T ShowPull<T>() where T : notnull, AbstractDialogViewModel
    {
        var dialogViewModel = App.Resolve<T>();
        ShowViewModel(dialogViewModel);
        return dialogViewModel;
    }

    public static void ShowMessageBox(string message, int backCount = 1, bool isCloseDialogViewOnAccept = false)
    {
        DialogMessageBoxInformationViewModel messageBoxViewModel;
        if (isCloseDialogViewOnAccept) messageBoxViewModel = new(message, Close);
        else messageBoxViewModel = new(message, () => BackViewModel(backCount));
        ShowViewModel(messageBoxViewModel);
    }

    public static void ShowMessageBoxConfirmation(Action onAgreement , string message)
    {
        var messageBoxViewModel = new DialogMessageBoxConfirmationViewModel(onAgreement, message);
        ShowViewModel(messageBoxViewModel);
    }

    public static void Close()
    {
        if(DialogHost.IsDialogOpen(_dialogHostName))
            DialogHost.Close(_dialogHostName);
    }
}