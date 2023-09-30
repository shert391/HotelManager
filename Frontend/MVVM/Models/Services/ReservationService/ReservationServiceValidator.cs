using DataContract.BusinessModels;
using DataContract.BusinessModels.Validators;

namespace HotelManager.MVVM.Models.Services.ReservationService;

public class ReservationServiceValidator : HotelValidatorBase, IReservationServiceValidator
{
    public bool CanReserved(Reservation newReservation)
    {
        return DefaultValidate(newReservation, typeof(ReservationValidator));
    }
}