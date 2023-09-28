using DevExpress.Mvvm;
using HotelManager.MVVM.Models.Builders;
using HotelManager.MVVM.Models.DataContract;
using HotelManager.MVVM.Models.DataContract.Requests;
using HotelManager.MVVM.Utils;
using System.Windows.Input;
using HotelManager.MVVM.Models.Services.RoomServices;

namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

internal class RoomEditorDialogViewModel : AbstractDialogViewModel, IConfigurable<IRoom>
{
    private IRoomService _roomService;
    public RoomChangeRequest RoomChangeRequest { get; } = new();

    public ICommand EditRoomCommand { get; }

    public ICommand CancelCommand { get; }

    public RoomEditorDialogViewModel(IRoomService roomService)
    {
        _roomService = roomService;
        EditRoomCommand = new DelegateCommand(() => _roomService.EditRoom(RoomChangeRequest,
            DefaultValidatorConfigBuilder.Create()
                .AddShowMessageBoxSuccessError("Комната успешно изменена!", isCloseSuccess: true).Build()));

        CancelCommand = new DelegateCommand(DialogHostController.Close);
    }

    public void Configurate(IRoom targetRoomViewModel) {
        RoomChangeRequest.NewType = targetRoomViewModel.Type;
        RoomChangeRequest.NewPrice = targetRoomViewModel.Price;
        RoomChangeRequest.NumberTargetRoom = targetRoomViewModel.Number;
    }
}