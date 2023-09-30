using DataContract.ViewModelsDto;

namespace HotelManager.MVVM.Models.Services.ReservationService;

public interface IReservationService
{
    public bool Reserved(ReservationViewModel newReservationDto, int roomNumber);
    public T GetReservationInfo<T>(Func<ReservationViewModel, T> expression, int roomNumber);
}