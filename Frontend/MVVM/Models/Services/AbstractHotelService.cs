using System.Collections.ObjectModel;
using System.Collections.Specialized;
using AutoMapper;
using DataContract.BusinessModels;
using HotelManager.InitApp;

namespace HotelManager.MVVM.Models.Services;

public class AbstractHotelService
{
    protected readonly IGlobalLocalStorage GlobalLocalStorage = App.Resolve<IGlobalLocalStorage>();
    protected readonly IMapper Mapper = App.Resolve<IMapper>();
    
    protected readonly ObservableCollection<Room> Rooms;
    protected readonly ObservableCollection<Room> RoomsBackup;

    protected AbstractHotelService()
    {
        RoomsBackup = GlobalLocalStorage.RoomsBackup;
        Rooms = GlobalLocalStorage.Rooms;   
        RuntimeUpdater.Update += Update;
        Rooms.CollectionChanged += OnRoomCollectionChanged;
    }

    protected virtual void OnRoomCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    { }

    protected virtual void Update() { }
}