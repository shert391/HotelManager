using System.Collections.Specialized;
using DevExpress.Mvvm.Native;
using System.ComponentModel;
using System.Windows.Data;
using HotelManager.InitApp;
using HotelManager.MVVM.Models.Services.FinanceService;
using HotelManager.MVVM.Models.Services.PostmanService;
using HotelManager.MVVM.Models.Services.ReservationService;
using HotelManager.MVVM.Models.Services.RoomService;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Security.Cryptography.Xml;
using DataContract.DTO.MappingEntities;
using DataContract.DTO.Messages;
using DataContract.DTO.ViewModels;
using HotelManager.MVVM.Models.Services.ApplicationService;
using HotelManager.MVVM.Models.Services.DebugHelperService;

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
    
    protected readonly ObservableCollection<RoomViewModel> Rooms; 
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
            case NeedPaymentMessage payInfo:
                RequestPayment(payInfo);
                break;
            case NewRoomReservationMessage reservationInfo:
                Rooms[Rooms.IndexOf(room => room.Number == reservationInfo.RoomNumber)].CurrentState = RoomState.Busy;
                break;
            case SuccessfulPaymentRoomMessage successfulPaymentInfo:
                Rooms[Rooms.IndexOf(room => room.Number == successfulPaymentInfo.RoomNumber)].CurrentState = RoomState.Free;
                break;
        }
    } 

    protected virtual void RequestPayment(NeedPaymentMessage payInformation) {
        var targetRoom = Rooms[Rooms.IndexOf(room => room.Number == payInformation.NumberRoom)];
        if (targetRoom.CurrentState == RoomState.NeedPaid) return;
        targetRoom.CurrentState = RoomState.NeedPaid;
        targetRoom.NeedPayment = payInformation;
    }

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