using System.Collections.ObjectModel;
using DataContract.DTO;
using DataContract.DTO.MappingEntities;
using DataContract.DTO.ViewModels;
using DataContract.Extensions;
using HotelManager.MVVM.Models.Services.ReservationService;

namespace HotelManager.MVVM.Models.Services.ApplicationService;

public class ApplicationService : AbstractHotelService, IApplicationService
{
    private readonly IReservationService _reservationService;

    public ApplicationService(IReservationService reservationService) => _reservationService = reservationService;

    public int FindBestRoom(ApplicationViewModel application)
    {
        var bestType = Rooms.Where(room => room.Type == application.Type && room.Reservation is null);

        var bestRoomNumber = -1; var minDelta = int.MaxValue;
        
        foreach (var room in bestType)
        {
            var currentDelta = room.Type.GetMaxPeople() - application.Peoples.Count;
            if (currentDelta >= minDelta) continue;
            minDelta = currentDelta;
            bestRoomNumber = room.Number;
        }

        return bestRoomNumber;
    }
    
    public bool Handle(ApplicationViewModel application)
    {
        var numberRoom = FindBestRoom(application);

        if (numberRoom == -1)
            return false;

        if (application.Peoples.Count <= 0)
            throw new ArgumentException("?!");
        
        _reservationService.Reserved(new ReservationViewModel
        {
            EndData = application.EndData,
            Peoples = application.Peoples,
        }, numberRoom, true);

        return true;
    }
}