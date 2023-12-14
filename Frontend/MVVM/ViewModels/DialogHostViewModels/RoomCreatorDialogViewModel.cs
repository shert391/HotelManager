using DevExpress.Mvvm;
using System.Windows.Input;
using DataContract.DTO.ViewModels;
using HotelManager.MVVM.Models.Services.RoomService;

namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

class RoomCreatorDialogViewModel : AbstractDialogViewModel
{
    private readonly IRoomService _roomService;

    public RoomViewModel NewRoomDto { get; set; } = new();

    public ICommand CreateRoomCommand { get; }

    public RoomCreatorDialogViewModel(IRoomService roomService)
    {
        _roomService = roomService;
        CreateRoomCommand = new DelegateCommand(CreateRoom);
    }

    private void CreateRoom() => _roomService.AddRoom(NewRoomDto);
}

