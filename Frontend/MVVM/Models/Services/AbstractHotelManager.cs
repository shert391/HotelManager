using System.Collections.ObjectModel;
using System.Collections.Specialized;
using AutoMapper;
using DataContract.BusinessModels;
using HotelManager.InitApp;

namespace HotelManager.MVVM.Models.Services;

public class AbstractHotelManager
{
    protected readonly IGlobalLocalStorage GlobalLocalStorage = App.Resolve<IGlobalLocalStorage>();
    protected readonly IMapper Mapper = App.Resolve<IMapper>();
    protected readonly ObservableCollection<Room> Rooms;

    protected AbstractHotelManager()
    {
        Rooms = GlobalLocalStorage.Rooms;   
        Rooms.CollectionChanged += OnRoomCollectionChanged;
    }

    protected virtual void OnRoomCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    { }
}