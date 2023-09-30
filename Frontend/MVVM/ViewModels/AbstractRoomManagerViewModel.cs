using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Data;
using DataContract.Extensions;
using DataContract.ViewModelsDto;
using HotelManager.InitApp;
using HotelManager.MVVM.Models.Services.PostmanService;
using HotelManager.MVVM.Models.Services.PostmanService.Messages;
using HotelManager.MVVM.Models.Services.ReservationService;
using HotelManager.MVVM.Models.Services.RoomService;
using HotelManager.MVVM.Models.Services.TestService;
using PropertyChanged;

namespace HotelManager.MVVM.ViewModels;

[SuppressPropertyChangedWarnings]
public abstract class AbstractRoomManagerViewModel : BaseViewModel
{
    protected readonly IRoomService RoomService = App.Resolve<IRoomService>();
    protected readonly ITestService TestService = App.Resolve<ITestService>();
    private readonly IPostmanService _postmanService = App.Resolve<IPostmanService>();
    
    protected readonly IReservationService ReservationService = App.Resolve<IReservationService>();
    
    protected readonly ExtendedObservableCollection<RoomViewModel> Rooms; 
    public ICollectionView RoomsView { get; }

    protected AbstractRoomManagerViewModel()
    {
        Rooms = RoomService.GetRoomsForViewModel();
        _postmanService.NewMessage += OnNewMessage;
        RoomsView = CollectionViewSource.GetDefaultView(Rooms);
        Rooms.ItemPropertyChanged += () => RoomsView?.Refresh();
        RoomService.RoomCollectionChanged += OnRoomCollectionChanged;
    }

    private void OnNewMessage(IMessage message)
    {
        if (message is PayInformation payInfo) RequestPayment(payInfo);
    }

    protected virtual void RequestPayment(PayInformation payInformation) { }

    private void OnRoomCollectionChanged(RoomViewModel? roomViewModel, NotifyCollectionChangedAction action,
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
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(action), action, null);
        }
    }
}