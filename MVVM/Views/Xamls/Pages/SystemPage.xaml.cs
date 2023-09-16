using HotelManager.InitApp;
using HotelManager.MVVM.ViewModels.DialogHostViewModels;
using MaterialDesignThemes.Wpf;
using System.Windows.Controls;

namespace HotelManager.MVVM.Views.Xamls.Pages;
public partial class SystemPage : UserControl
{
    public SystemPage() => InitializeComponent();

    private void AddRoomButton_Click(object sender, System.Windows.RoutedEventArgs e) => DialogHost.Show(App.Resolve<RoomCreatorViewModel>());
}
