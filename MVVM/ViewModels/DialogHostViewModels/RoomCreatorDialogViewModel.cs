using DevExpress.Mvvm;
using HotelManager.MVVM.Models.Builders;
using HotelManager.MVVM.Models.DataContract;
using HotelManager.MVVM.Models.Services;
using HotelManager.MVVM.Models.Services.RoomServices;
using HotelManager.MVVM.Utils;
using System.Windows.Input;

namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

class RoomCreatorDialogViewModel : AbstractDialogManagerViewModel
{
    public int Number { get; set; } = 1;

    public decimal Price { get; set; } = 3000;

    public RoomType Type { get; set; }

    public ICommand CreateRoomCommand { get; }

    public ICommand CancelCommand { get; }

    public RoomCreatorDialogViewModel()
    {
        CreateRoomCommand = new DelegateCommand(CreateRoom);
        CancelCommand = new DelegateCommand(DialogHostController.Close);
    }

    private void CreateRoom()
    {
        RoomService.AddRoom(new Room
        {
            Number = Number,
            Price = Price,
            Type = Type,
        },
        DefaultValidatorConfigBuilder.Create().AddShowMessageBoxSuccessError("Комната успешно создана!").Build());
    }
}

