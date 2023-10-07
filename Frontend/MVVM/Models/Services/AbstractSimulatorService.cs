using System.Collections.ObjectModel;
using System.Collections.Specialized;
using DataContract.ViewModelsDto;

namespace HotelManager.MVVM.Models.Services;

public abstract class AbstractSimulatorService : AbstractHotelService
{
    public ObservableCollection<RoomViewModel> RoomsSnapshot { get; } = new();

    protected AbstractSimulatorService()
    {
        foreach (var room in Rooms)
            RoomsSnapshot.Add(Mapper.Map<RoomViewModel>(room));
    }

    protected override void OnRoomCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                foreach (var newItem in e.NewItems!)
                    RoomsSnapshot.Add(Mapper.Map<RoomViewModel>(newItem));
                break;
            case NotifyCollectionChangedAction.Remove:
                RoomsSnapshot.RemoveAt(e.OldStartingIndex);
                break;
            case NotifyCollectionChangedAction.Replace:
                RoomsSnapshot[e.OldStartingIndex] = Mapper.Map<RoomViewModel>(e.NewItems![0]);
                break;
            case NotifyCollectionChangedAction.Move:
                break;
            case NotifyCollectionChangedAction.Reset:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}