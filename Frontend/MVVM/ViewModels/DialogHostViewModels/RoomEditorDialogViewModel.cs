using DevExpress.Mvvm;
using System.Windows.Input;
using DataContract.DTO.ViewModels;
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