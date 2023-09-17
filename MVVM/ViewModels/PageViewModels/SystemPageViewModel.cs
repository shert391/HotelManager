using DevExpress.Mvvm;
using HotelManager.MVVM.Models.DataContract;
using HotelManager.MVVM.Models.Services;
using HotelManager.MVVM.Utils;
using HotelManager.MVVM.ViewModels.DialogHostViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HotelManager.MVVM.ViewModels.PageViewModels;

public class SystemPageViewModel : BaseViewModel
{
    public ReadOnlyObservableCollection<Room> Rooms { get; }

    public ICommand AddRoomCommand { get; }

    private readonly IRoomService _roomService;

    public SystemPageViewModel(IRoomService roomService)
    {
        AddRoomCommand = new DelegateCommand(() => DialogHostController.Show<RoomCreatorViewModel>() );

        Rooms = roomService.GetRooms();
        _roomService = roomService;
    }
}

