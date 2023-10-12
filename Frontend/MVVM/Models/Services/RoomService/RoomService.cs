using DataContract.BusinessModels;
using DataContract.ViewModelsDto;
using System.Collections.Specialized;
using HotelManager.MVVM.Utils;
using System.Collections.ObjectModel;

namespace HotelManager.MVVM.Models.Services.RoomService;

public class RoomService : AbstractHotelService, IRoomService
{
    private readonly IRoomServiceValidator _roomServiceValidator;
    
    public RoomService(IRoomServiceValidator roomServiceValidator) => _roomServiceValidator = roomServiceValidator;

    public event Action<RoomViewModel?, NotifyCollectionChangedAction, int, int>? RoomCollectionChanged;

    protected override void OnRoomCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        RoomCollectionChanged?.Invoke(Mapper.Map<RoomViewModel>(e.NewItems?[0]), e.Action, e.NewStartingIndex, e.OldStartingIndex);
    }
    
    public ObservableCollection<RoomViewModel> GetRoomsForViewModel()
    {
        ObservableCollection<RoomViewModel> result = new();

        foreach (var room in GlobalLocalStorage.Rooms)
            result.Add(Mapper.Map<RoomViewModel>(room));

        return result;
    }

    public void AddRoom(RoomViewModel newRoomDto)
    {
        var room = Mapper.Map<Room>(newRoomDto);
        if (!_roomServiceValidator.CanAddRoom(room)) return;
        Rooms.Add(room);
        DialogHostController.ShowMessageBox("Комната успешно добавлена!", isCloseDialogViewOnAccept: true);
    }

    public void DeleteRoom(int roomNumber)
    {
        var index = GlobalLocalStorage.GetRoomIndexInArray(roomNumber);
        Rooms.RemoveAt(index);
        DialogHostController.ShowMessageBox("Комната успешно удалена!", isCloseDialogViewOnAccept: true);
    }

    public void EditRoom(RoomViewModel newRoomDto)
    {
        var newRoom = Mapper.Map<Room>(newRoomDto);
        var index = GlobalLocalStorage.GetRoomIndexInArray(newRoomDto.Number);
        if (!_roomServiceValidator.CanEditRoom(newRoom)) return;
        Rooms[index] = newRoom;
        DialogHostController.ShowMessageBox("Комната успешно изменена", isCloseDialogViewOnAccept: true);
    }

    public void CreateBackup()
    {
        RoomsBackup.Clear();
        foreach (var room in Rooms)
            RoomsBackup.Add(Mapper.Map<Room>(room));
    }

    public void ReturnBackup()
    {
        Rooms.Clear();
        foreach (var roomBackup in RoomsBackup)
            Rooms.Add(Mapper.Map<Room>(roomBackup));
        RoomsBackup.Clear();
        GlobalLocalStorage.AddHoursForTest = 0;
    }
};