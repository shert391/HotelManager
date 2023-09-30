using DataContract.BusinessModels;
using DataContract.ViewModelsDto;
using HotelManager.MVVM.Models.Services.PostmanService.Messages;
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

    protected override void Update()
    {
        foreach (var room in Rooms)
            if (room.Reservation is not null)
                if (IsReservationExpired(room.Reservation))
                    PostmanService.SendNewMessage(new PayInformation
                    {
                        Price = GeneratePrice(room), 
                        NumberRoom = room.Number,
                    });
    }

    /// <summary>
    /// <b>GeneratePrice</b> - вычисляет итоговую цену для оплаты.
    /// </summary>
    /// <returns></returns>
    private decimal GeneratePrice(Room room)
    {
        var delta = (room.Reservation!.EndData - room.Reservation!.StartData).TotalDays;
        var price = delta * (double)room.Price;
        return (decimal)price;
    }
    private bool IsReservationExpired(Reservation reservation) => DateTime.Now >= reservation.EndData;
    
    public T GetReservationInfo<T>(Func<ReservationViewModel, T> expression, int roomNumber)
    {
        return expression(Mapper.Map<ReservationViewModel>(GlobalLocalStorage.GetRoom(roomNumber)!.Reservation));
    }
}