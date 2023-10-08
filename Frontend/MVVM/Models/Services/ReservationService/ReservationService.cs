using DataContract.BusinessModels;
using DataContract.ViewModelsDto;
using DataContract.ViewModelsDto.Messages;
using HotelManager.MVVM.Models.Services.FinanceService;
using HotelManager.MVVM.Models.Services.PostmanService;
using HotelManager.MVVM.Utils;

namespace HotelManager.MVVM.Models.Services.ReservationService;

public class ReservationService : AbstractHotelService, IReservationService
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

    public void Reserved(ReservationViewModel newReservationDto, int roomNumber, bool isTest = false)
    {
        var reservation = Mapper.Map<Reservation>(newReservationDto);
        if (!isTest && !_reservationServiceValidator.CanReserved(reservation)) return;
        var index = GlobalLocalStorage.GetRoomIndexInArray(roomNumber);
        Rooms[index].Reservation = reservation;
        _postmanService.SendNewMessage(new NewRoomReservationMessage { RoomNumber = roomNumber });
        if(!isTest) DialogHostController.ShowMessageBox("Комната успешно забронирована!", isCloseDialogViewOnAccept: true);
    }

    protected override void Update()
    {
        foreach (var room in Rooms)
            if (room.Reservation is not null)
                if (IsReservationExpired(room.Reservation))
                    _postmanService.SendNewMessage(new NeedPaymentMessage
                    {
                        Price = _financeService.GetRoomTotalPrice(room),
                        Fines = _financeService.GetRoomFinePrice(room),
                        NumberRoom = room.Number,
                    });
    }

    private bool IsReservationExpired(Reservation reservation) => DateTime.Now.AddHours(GlobalLocalStorage.AddHoursForTest) >= reservation.EndData;

    public T GetReservationInfo<T>(Func<ReservationViewModel, T> expression, int roomNumber)
    {
        return expression(Mapper.Map<ReservationViewModel>(GlobalLocalStorage.GetRoom(roomNumber)!.Reservation));
    }
}