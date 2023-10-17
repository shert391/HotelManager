using DevExpress.Mvvm;
using HotelManager.MVVM.Models.Services.DebugHelperService;
using HotelManager.MVVM.Utils;
using System.Windows.Input;

namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

public class SettingsGeneratorViewModel : AbstractDialogViewModel
{
    public int MaxRooms { get; set; } = 500;
    public int MinRooms { get; set; } = 400;
    
    public ICommand GenerateRoomsCommand { get; }
    private readonly IDebugHelperService _debugHelperService;

    public SettingsGeneratorViewModel(IDebugHelperService debugHelperService)
    {
        GenerateRoomsCommand = new DelegateCommand(GenerateRooms);
        _debugHelperService = debugHelperService;
    }

    private void GenerateRooms()
    {
        if (_debugHelperService.GenerateTestRooms(MinRooms, MaxRooms))
            DialogHostController.Close(); 
    }
}
