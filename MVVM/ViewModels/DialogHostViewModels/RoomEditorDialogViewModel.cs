using DevExpress.Mvvm;
using HotelManager.MVVM.Models.Builders;
using HotelManager.MVVM.Models.DataContract;
using HotelManager.MVVM.Models.DataContract.Requests;
using HotelManager.MVVM.Models.Services;
using HotelManager.MVVM.Models.Services.RoomServices;
using HotelManager.MVVM.Utils;
using System.Windows.Input;

namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

internal class RoomEditorDialogViewModel : AbstractDialogManagerViewModel, IConfigurable<Room>
{
    public RoomChangeRequest RoomChangeRequest { get; } = new();

    public ICommand EditRoomCommand { get; }

    public ICommand CancelCommand { get; }

    public RoomEditorDialogViewModel()
    {
        EditRoomCommand = new DelegateCommand(() => RoomService.EditRoom(RoomChangeRequest,
            DefaultValidatorConfigBuilder.Create()
                .AddShowMessageBoxSuccessError("Комната успешно изменена!", isCloseSuccess: true).Build()));

        CancelCommand = new DelegateCommand(DialogHostController.Close);
    }

    public void Configurate(Room targetRoom) {
        RoomChangeRequest.NewType = targetRoom.Type;
        RoomChangeRequest.NewPrice = targetRoom.Price;
        RoomChangeRequest.NumberTargetRoom = targetRoom.Number;
    }
}