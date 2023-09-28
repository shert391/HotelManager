using DevExpress.Mvvm;
using HotelManager.MVVM.Models.Builders;
using HotelManager.MVVM.Models.DataContract;
using HotelManager.MVVM.Utils;
using System.Windows.Input;
using HotelManager.MVVM.Models.Services.RoomServices;

namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

class RoomCreatorDialogViewModel : AbstractDialogViewModel
{
    private IRoomService _roomService;
    
    public int Number { get; set; } = 1;

    public decimal Price { get; set; } = 3000;

    public RoomType Type { get; set; }

    public ICommand CreateRoomCommand { get; }

    public ICommand CancelCommand { get; }

    public RoomCreatorDialogViewModel(IRoomService roomService)
    {
        _roomService = roomService;
        CreateRoomCommand = new DelegateCommand(CreateRoom);
        CancelCommand = new DelegateCommand(DialogHostController.Close);
    }

    private void CreateRoom()
    {
        _roomService.AddRoom(new Room
        {
            Number = Number,
            Price = Price,
            Type = Type,
        },
        DefaultValidatorConfigBuilder.Create().AddShowMessageBoxSuccessError("Комната успешно создана!").Build());
    }
}

