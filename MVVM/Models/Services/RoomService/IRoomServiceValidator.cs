using HotelManager.MVVM.Utils;
using HotelManager.MVVM.Models.Builders;
using HotelManager.MVVM.Models.DataContract;

namespace HotelManager.MVVM.Models.Services.RoomService;
public interface IRoomServiceValidator : IConfigurable<IRoomServiceValidatorConfig>
{
    public void AddRoom(Room addRoom, Action addMethod);
    public void EditRoom(Room newRoom, Action editMethod);
}

