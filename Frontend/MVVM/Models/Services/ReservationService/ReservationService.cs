using DataContract.BusinessModels;
using DataContract.ViewModelsDto;
using DataContract.ViewModelsDto.Messages;
using HotelManager.MVVM.Models.Services.FinanceService;
using HotelManager.MVVM.Models.Services.PostmanService;
using HotelManager.MVVM.Utils;

namespace HotelManager.MVVM.Models.Services.ReservationService;

public class ReservationService : AbstractHotelManager, IReservationService
{
    private readonly IReservationServiceValidator _reservationServiceValidator;
    private readonly IFinanceService _financeService;
    private readonly IPostmanService _postmanService;

    public ReservationService(IReservationServiceValidator reservationServiceValidator, IFinanceService financeService,
        IPostmanService postmanService)
    {
        _reservationServiceValidator = reservationServiceValidator;
        _financeService = financeService;
        _postmanService = postmanService;
    }

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
                    _postmanService.SendNewMessage(new PayInformationDto
                    {
                        Price = _financeService.GetTotalPrice(room),
                        Fines = _financeService.GetFinePrice(room),
                        NumberRoom = room.Number,
                    });
    }

    private bool IsReservationExpired(Reservation reservation) => DateTime.Now >= reservation.EndData;

    public T GetReservationInfo<T>(Func<ReservationViewModel, T> expression, int roomNumber)
    {
        return expression(Mapper.Map<ReservationViewModel>(GlobalLocalStorage.GetRoom(roomNumber)!.Reservation));
    }
}