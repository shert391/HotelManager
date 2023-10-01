using DevExpress.Mvvm;
using HotelManager.MVVM.Utils;
using System.Windows.Input;
using DataContract.ViewModelsDto;
using HotelManager.MVVM.Models.Services.RoomService;

namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

internal class RoomEditorDialogViewModel : AbstractDialogViewModel
{
    private IRoomService _roomService;
    public RoomViewModel NewRoomDto { get; set; } = new();
    public ICommand EditRoomCommand { get; }

    public RoomEditorDialogViewModel(IRoomService roomService)
    {
        _roomService = roomService;
        EditRoomCommand = new DelegateCommand(() => _roomService.EditRoom(NewRoomDto));
    }
}