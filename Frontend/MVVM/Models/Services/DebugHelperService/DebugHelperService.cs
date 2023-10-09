using System.Collections.ObjectModel;
using DataContract.BusinessModels;
using DataContract.Extensions;
using DataContract.GlobalConstants;
using DataContract.ViewModelsDto;

namespace HotelManager.MVVM.Models.Services.DebugHelperService;

public class DebugHelperService : AbstractHotelService, IDebugHelperService
{
    private readonly Random _random = new();

    private ObservableCollection<PeopleViewModel> GeneratePeoples(int count)
    {
        var result = new ObservableCollection<PeopleViewModel>();

        for (var i = 0; i < count; i++)
        {
            result.Add(new PeopleViewModel()
            {
                Age = _random.Next(5, 95),
                FullName = Faker.Name.FullName(),
                NumberPassport = _random.Next(100000,999999).ToString(),
                SeriesPassport = _random.Next(1000,9999).ToString(),
                ResidenceAddress = Faker.Country.Name(),
                PhoneNumber = Faker.Phone.Number(),
            });
        }

        return result;
    }
    
    public void GenerateTestRooms()
    {
        for (var i = 1; i <= _random.Next(RoomConstants.MinRoomNumber, RoomConstants.MaxRoomNumber); i++)
        {
            var newRoom = new Room
            {
                Number = i,
                Price = _random.Next((int)RoomConstants.MinPrice, (int)RoomConstants.MaxPrice),
                Type = (RoomType)_random.Next(0, Enum.GetNames(typeof(RoomType)).Length)
            };
            if(GlobalLocalStorage.GetRoom(i) is null)
                Rooms.Add(newRoom);
        }
    }

    public void AddHoursToStorageTime(int countHours) => GlobalLocalStorage.AddHoursForTest += countHours;
    
    public IEnumerable<ApplicationDto> GenerateApplications(int countApplication)
    {
        var result = new List<ApplicationDto>();
        
        for (var i = 0; i < countApplication; i++)
        {
            var roomType = (RoomType)_random.Next(0, Enum.GetNames(typeof(RoomType)).Length - 1);
            
            result.Add(new ApplicationDto
            {
                Peoples = GeneratePeoples(_random.Next(1, roomType.GetMaxPeople())),
                EndData = DateTime.Now.AddDays(_random.Next(0,5)).AddHours(_random.Next(5, 24)),
                Type = roomType,
            });
        }

        return result;
    }
}