using DevExpress.Mvvm;
using HotelManager.MVVM.Models.DataContract;
using HotelManager.MVVM.Models.Services;
using HotelManager.MVVM.Utils;
using System.Windows.Input;

namespace HotelManager.MVVM.ViewModels.DialogHostViewModels;

class RoomCreatorViewModel : BaseViewModel, IDialogViewModel
{
    public int Number { get; set; }

    public decimal Price { get; set; }

    public RoomType Type { get; set; }

    public ICommand CreateRoomCommand { get; }

    public ICommand CancelCommand { get; }

    private readonly IRoomService _roomService;

    public RoomCreatorViewModel(IRoomService roomService)
    {
        CreateRoomCommand = new DelegateCommand(CreateRoom);
        CancelCommand = new DelegateCommand(() => DialogHostController.Close());
        _roomService = roomService;
    }

    private void CreateRoom()
    {
        _roomService.AddRoom(new Room
        {
            Number = Number,
            Price = Price,  
            Type = Type,
        });
    }
}

