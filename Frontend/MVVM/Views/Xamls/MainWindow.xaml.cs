using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using HotelManager.MVVM.ViewModels;

namespace HotelManager.MVVM.Views.Xamls;

public partial class MainWindow
{
    public MainWindow(MainWindowViewModel mainMenuViewModel)
    {
        InitializeComponent();
        DataContext = mainMenuViewModel;
    }
    private void CloseApplication(object sender, RoutedEventArgs e) => Process.GetCurrentProcess().Kill();
    private void MinimizeWindow(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;
    private void WindowMove(object sender, MouseButtonEventArgs e) { if (Mouse.LeftButton == MouseButtonState.Pressed) DragMove(); }
}
