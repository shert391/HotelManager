using HotelManager.InitApp;
using MaterialDesignThemes.Wpf;
using HotelManager.MVVM.ViewModels.DialogHostViewModels;

namespace HotelManager.MVVM.Utils;

public static class DialogHostController
{
    private const string _dialogHostName = "MainDialogHost";
    private static IDialogViewModel? _previewContent;

    private static void ReplaceViewModel(IDialogViewModel newViewModel)
    {
        var dialogSession = DialogHost.GetDialogSession(_dialogHostName);

        if (dialogSession is null)
        {
            DialogHost.Show(newViewModel, _dialogHostName);
            return;
        }

        _previewContent = dialogSession.Content as IDialogViewModel;
        dialogSession.UpdateContent(newViewModel);
    }

    public static void Show<T>() where T : notnull, IDialogViewModel
    {
        var dialogViewModel = App.Resolve<T>();
        ReplaceViewModel(dialogViewModel);
    }

    public static void ShowMessageBox(string message)
    {
        var messageBoxViewModel = new MessageBoxViewModel(message);
        ReplaceViewModel(messageBoxViewModel);
    }

    public static void Close()
    {
        var dialogSession = DialogHost.GetDialogSession(_dialogHostName);

        if (dialogSession is null) return;

        _previewContent = dialogSession.Content as IDialogViewModel;
        DialogHost.Close(_dialogHostName);
    }

    public static void ShowPreview()
    {
        if (_previewContent is null)
            Close();
        else
            ReplaceViewModel(_previewContent);
    }
}