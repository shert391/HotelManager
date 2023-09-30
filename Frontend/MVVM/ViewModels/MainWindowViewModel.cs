using DevExpress.Mvvm;
using HotelManager.InitApp;
using HotelManager.MVVM.ViewModels.PageViewModels;
using System.Windows.Input;

namespace HotelManager.MVVM.ViewModels;

public class MainWindowViewModel : BaseViewModel
{
    public BaseViewModel CurrentViewModel { get; private set; } = App.Resolve<SystemPageViewModel>();
    public ICommand SelectSystemView { get; }
    public ICommand SelectTestView { get; }

    public MainWindowViewModel()
    {
        SelectSystemView = new DelegateCommand(() => CurrentViewModel = App.Resolve<SystemPageViewModel>());
        SelectTestView = new DelegateCommand(() => CurrentViewModel = App.Resolve<TestPageViewModel>());
    }
}

