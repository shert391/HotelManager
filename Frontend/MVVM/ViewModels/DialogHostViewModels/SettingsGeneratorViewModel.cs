using DataContract.ViewModelsDto;
using DevExpress.Mvvm;
using HotelManager.MVVM.Models.Services.DebugHelperService;
using HotelManager.MVVM.Utils;
using System.Windows.Input;

namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

public class SettingsGeneratorViewModel : AbstractDialogViewModel
{
    public ICommand GenerateRoomsCommand { get; }
    private readonly IDebugHelperService _debugHelperService;
    public RoomGenerationSettingsDto RoomGenerationSettingsDto { get; } = new();

    public SettingsGeneratorViewModel(IDebugHelperService debugHelperService)
    {
        _debugHelperService = debugHelperService;
        GenerateRoomsCommand = new DelegateCommand(() =>
        {
            _debugHelperService.GenerateTestRooms(RoomGenerationSettingsDto);
            DialogHostController.Close();
        });
    }
}
