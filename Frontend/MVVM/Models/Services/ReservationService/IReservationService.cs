using DataContract.DTO.MappingEntities;
using DataContract.DTO.ViewModels;

namespace HotelManager.MVVM.Models.Services.ReservationService;

public interface IReservationService
{
    public void Reserved(ReservationViewModel newReservationDto, int roomNumber, bool isTest = false);
    public T GetReservationInfo<T>(Func<ReservationViewModel, T> expression, int roomNumber);
}