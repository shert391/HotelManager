using DataContract.BusinessModels;
using DataContract.ViewModelsDto;
using HotelManager.MVVM.Utils;

namespace HotelManager.MVVM.Models.Services.ReservationService;

public class ReservationService : AbstractHotelManager, IReservationService
{
    private readonly IReservationServiceValidator _reservationServiceValidator;

    public ReservationService(IReservationServiceValidator reservationServiceValidator) => _reservationServiceValidator = reservationServiceValidator;

    public bool Reserved(ReservationViewModel newReservationDto, int roomNumber)
    {
        var reservation = Mapper.Map<Reservation>(newReservationDto);
        if (!_reservationServiceValidator.CanReserved(reservation)) return false;
        var index = GlobalLocalStorage.GetRoomIndexInArray(roomNumber);
        Rooms[index].Reservation = reservation;
        DialogHostController.ShowMessageBox("Комната успешно забронирована!", isCloseDialogViewOnAccept: true);
        return true;
    }

    public T GetReservationInfo<T>(Func<ReservationViewModel, T> expression, int roomNumber)
    {
        return expression(Mapper.Map<ReservationViewModel>(GlobalLocalStorage.GetRoom(roomNumber)!.Reservation));
    }
}