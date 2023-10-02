using System.Windows.Input;
using DevExpress.Mvvm;
using HotelManager.MVVM.Utils;
using HotelManager.MVVM.ViewModels.DialogHostViewModels;

namespace HotelManager.MVVM.ViewModels.PageViewModels;

public class TestPageViewModel : BaseViewModel
{
    public ICommand ShowSettingSystemCommand { get; }
    private readonly TestingSettingsViewModel _settingsViewModel;

    public TestPageViewModel(TestingSettingsViewModel settingsViewModel)
    {
        _settingsViewModel = settingsViewModel;
        ShowSettingSystemCommand = new DelegateCommand(DialogHostController.Show<TestingSettingsViewModel>);
    }
}

