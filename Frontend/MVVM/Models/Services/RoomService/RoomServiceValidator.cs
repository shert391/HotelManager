using DataContract.BusinessModels;
using DataContract.BusinessModels.Validators;
using HotelManager.MVVM.Utils;

namespace HotelManager.MVVM.Models.Services.RoomService;

public class RoomServiceValidator : HotelValidatorBase, IRoomServiceValidator
{
    public bool CanAddRoom(Room newRoom)
    {
        if (!DefaultValidate(newRoom, typeof(RoomValidator)))
            return false;

        if (GlobalLocalStorage.Rooms.All(x => x.Number != newRoom.Number)) return true;
        
        DialogHostController.ShowMessageBox("Комната уже существует");
        return false;
    }

    public bool CanEditRoom(Room newRoom)
    {
        return DefaultValidate(newRoom, typeof(RoomValidator));
    }
}