using HotelManager.MVVM.Utils;
using HotelManager.MVVM.Models.DataContract;
using System.Collections.ObjectModel;

namespace HotelManager.MVVM.Models.Services.RoomService;
public interface IRoomServiceValidator : IConfigurable<RoomServiceValidatorConfig>
{
    public void AddRoom(Room addRoom, Action addMethod);
    public void EditRoom(Room editableRoom, Action editMethod);
}

