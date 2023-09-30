using System.Collections.ObjectModel;
using System.Collections.Specialized;
using AutoMapper;
using DataContract.BusinessModels;
using HotelManager.InitApp;
using HotelManager.MVVM.Models.Services.PostmanService;

namespace HotelManager.MVVM.Models.Services;

public class AbstractHotelManager
{
    protected readonly IGlobalLocalStorage GlobalLocalStorage = App.Resolve<IGlobalLocalStorage>();
    protected readonly IPostmanService PostmanService = App.Resolve<IPostmanService>();
    protected readonly IMapper Mapper = App.Resolve<IMapper>();
    protected readonly ObservableCollection<Room> Rooms;

    protected AbstractHotelManager()
    {
        Rooms = GlobalLocalStorage.Rooms;   
        RuntimeUpdater.RuntimeUpdate += Update;
        Rooms.CollectionChanged += OnRoomCollectionChanged;
    }

    protected virtual void OnRoomCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    { }

    protected virtual void Update() { }
}