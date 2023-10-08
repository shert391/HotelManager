using System.Collections.Specialized;
using DevExpress.Mvvm.Native;
using System.ComponentModel;
using System.Windows.Data;
using DataContract.Extensions;
using DataContract.ViewModelsDto;
using DataContract.ViewModelsDto.Messages;
using HotelManager.InitApp;
using HotelManager.MVVM.Models.Services.ApplicationService;
using HotelManager.MVVM.Models.Services.DebugHelperService;
using HotelManager.MVVM.Models.Services.FinanceService;
using HotelManager.MVVM.Models.Services.PostmanService;
using HotelManager.MVVM.Models.Services.ReservationService;
using HotelManager.MVVM.Models.Services.RoomService;
using PropertyChanged;

namespace HotelManager.MVVM.ViewModels;

[SuppressPropertyChangedWarnings]
public abstract class AbstractRoomManagerViewModel : BaseViewModel
{
    protected readonly IRoomService RoomService = App.Resolve<IRoomService>();
    protected readonly IFinanceService FinanceService = App.Resolve<IFinanceService>();
    protected readonly IDebugHelperService DebugHelperService = App.Resolve<IDebugHelperService>();
    protected readonly IReservationService ReservationService = App.Resolve<IReservationService>();
    protected readonly IApplicationService ApplicationService = App.Resolve<IApplicationService>();

    private readonly IPostmanService _postmanService = App.Resolve<IPostmanService>();
    
    protected readonly ExtendedObservableCollection<RoomViewModel> Rooms; 
    public ICollectionView RoomsView { get; }

    protected AbstractRoomManagerViewModel()
    {
        Rooms = RoomService.GetRoomsForViewModel();
        _postmanService.NewMessage += OnNewMessage;
        RoomsView = CollectionViewSource.GetDefaultView(Rooms);
        RoomService.RoomCollectionChanged += OnRoomServiceCollectionChanged;
    }

    private void OnNewMessage(IMessage message)
    {
        switch (message)
        {
            case PayInformationDto payInfo:
                RequestPayment(payInfo);
                break;
            case NewRoomReservation reservationInfo:
                Rooms[Rooms.IndexOf(room => room.Number == reservationInfo.RoomNumber)].CurrentState = RoomState.Busy;
                RoomsView.Refresh();
                break;
        }
    }

    protected virtual void RequestPayment(PayInformationDto payInformation) { }

    private void OnRoomServiceCollectionChanged(RoomViewModel? roomViewModel, NotifyCollectionChangedAction action,
        int newIndex, int oldIndex)
    {
        switch (action)
        {
            case NotifyCollectionChangedAction.Add:
                Rooms.Add(roomViewModel!);
                break;
            case NotifyCollectionChangedAction.Remove:
                Rooms.RemoveAt(oldIndex);
                break;
            case NotifyCollectionChangedAction.Replace:
                Rooms[newIndex] = roomViewModel!;
                break;
            case NotifyCollectionChangedAction.Move:
                break;
            case NotifyCollectionChangedAction.Reset:
                Rooms.Clear();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(action), action, null);
        }
    }
}