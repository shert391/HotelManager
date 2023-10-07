using DevExpress.Mvvm;
using HotelManager.InitApp;
using HotelManager.MVVM.ViewModels.PageViewModels;
using System.Windows.Input;

namespace HotelManager.MVVM.ViewModels;

public class MainWindowViewModel : BaseViewModel
{
    public BaseViewModel CurrentViewModel { get; private set; } = App.Resolve<SystemPageViewModel>();
    public ICommand SelectSystemViewCommand { get; }
    public ICommand SelectSimulatorPageViewCommand { get; }

    public MainWindowViewModel()
    {
        SelectSystemViewCommand = new DelegateCommand(() => CurrentViewModel = App.Resolve<SystemPageViewModel>());
        SelectSimulatorPageViewCommand = new DelegateCommand(() => CurrentViewModel = App.Resolve<SimulatorPageViewModel>());
    }
}

