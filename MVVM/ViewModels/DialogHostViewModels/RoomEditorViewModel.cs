using DevExpress.Mvvm;
using HotelManager.MVVM.Models.DataContract;
using HotelManager.MVVM.Models.Services;
using HotelManager.MVVM.Models.Services.RoomService;
using HotelManager.MVVM.Utils;
using System.Windows.Input;
using HotelManager.MVVM.Models.Builders;

namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

internal class RoomEditorViewModel : AbstractRoomManagerViewModel, IDialogViewModel, IConfigurable<Room>
{
    public Room NewRoom { get; set; } = new();

    public ICommand EditRoomCommand { get; }

    public ICommand CancelCommand { get; }

    public RoomEditorViewModel(IRoomService roomService, ITestService testService) : base(roomService, testService)
    {
        EditRoomCommand = new DelegateCommand(() => RoomService.EditRoom(NewRoom,
            RoomServiceValidatorConfigBuilder
            .Create()
            .AddActionOnError((error) => DialogHostController.ShowMessageBoxInformation(error))
            .AddActionOnSuccess(() => DialogHostController.ShowMessageBoxInformation("Комната успешно изменена!", true))
            .Build()));

        CancelCommand = new DelegateCommand(DialogHostController.Close);
    }

    public void Configurate(Room room) => NewRoom = room.Clone();
}