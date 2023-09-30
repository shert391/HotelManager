using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Data;
using DataContract.ViewModelsDto;
using HotelManager.InitApp;
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
    protected readonly IReservationService ReservationService = App.Resolve<IReservationService>();
    
    private readonly ObservableCollection<RoomViewModel> _rooms; // TODO: Наслоить оболочку с функционалом уведомления об изменении ссылочных типов
    public ICollectionView RoomsView { get; }

    protected AbstractRoomManagerViewModel()
    {
        _rooms = RoomService.GetRoomsForViewModel();
        // RoomService.NewMessage += NewMessageHandler;
        RoomsView = CollectionViewSource.GetDefaultView(_rooms);
        RoomService.RoomCollectionChanged += OnRoomCollectionChanged;
    }

    // protected virtual void NewMessageHandler(IRoomServiceMessage message) // TODO: надо декомпозировать
    // {
    //     if (message is not TimeEvictMessage evictMessage)
    //         return;
    //     
    //     var room = _rooms.First(room => room.Number == evictMessage.NumberRoom);
    //     
    //     room.CurrentState = RoomState.NeedPaid;
    //     room.NeedPaid = evictMessage.NeedPay;
    // }

    private void OnRoomCollectionChanged(RoomViewModel? roomViewModel, NotifyCollectionChangedAction action,
        int newIndex, int oldIndex)
    {
        switch (action)
        {
            case NotifyCollectionChangedAction.Add:
                _rooms.Add(roomViewModel!);
                break;
            case NotifyCollectionChangedAction.Remove:
                _rooms.RemoveAt(oldIndex);
                break;
            case NotifyCollectionChangedAction.Replace:
                _rooms[newIndex] = roomViewModel!;
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