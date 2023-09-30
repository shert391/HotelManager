using DataContract.BusinessModels;

namespace HotelManager.MVVM.Models.Services.ReservationService;

public interface IReservationServiceValidator
{
    public bool CanReserved(Reservation newReservation);
}