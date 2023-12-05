using System.Collections.ObjectModel;
using DataContract.Extensions;
using DataContract.ViewModelsDto;
using HotelManager.MVVM.Models.Services.ReservationService;

namespace HotelManager.MVVM.Models.Services.ApplicationService;

public class ApplicationService : AbstractHotelService, IApplicationService
{
    private readonly IReservationService _reservationService;

    public ApplicationService(IReservationService reservationService) => _reservationService = reservationService;

    public int FindBestRoom(ApplicationDto application)
    {
        var bestType = Rooms.Where(room => room.Type == application.Type && room.Reservation is null);

        var bestRoomNumber = -1; var minDelta = int.MaxValue;
        
        foreach (var room in bestType)
        {
            var currentDelta = room.Type.GetMaxPeople() - application.Peoples;
            if (currentDelta >= minDelta) continue;
            minDelta = currentDelta;
            bestRoomNumber = room.Number;
        }

        return bestRoomNumber;
    }
    
    public bool Handle(ApplicationDto application)
    {
        var numberRoom = FindBestRoom(application);

        if (numberRoom == -1)
            return false;

        _reservationService.Reserved(new ReservationViewModel
        {
            EndData = application.EndData,
            Peoples = new ObservableCollection<PeopleViewModel>()
        }, numberRoom, true);

        return true;
    }
}