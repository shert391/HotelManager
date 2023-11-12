using DataContract.BusinessModels;
using DataContract.Extensions;
using HotelManager.MVVM.Utils;
using System.Collections.ObjectModel;
using DataContract;
using DataContract.DTO.Settings;
using DataContract.DTO.ViewModels;

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
                NumberPassport = _random.Next(100000, 999999).ToString(),
                SeriesPassport = _random.Next(1000, 9999).ToString(),
                ResidenceAddress = Faker.Country.Name(),
                PhoneNumber = Faker.Phone.Number(),
            });
        }

        return result;
    }

    public void GenerateTestRooms(RoomGenerationSettingsDto roomGenerationSettingsDto)
    {
        for (var i = 1; i <= _random.Next(roomGenerationSettingsDto.Rooms.Min, roomGenerationSettingsDto.Rooms.Max); i++)
        {
            var type = (RoomType)_random.Next(0, Enum.GetNames(typeof(RoomType)).Length);

            var newRoom = new Room
            {
                Number = i,
                Type = type,
                Price = ReflectionEX.GetValueByNameProperty<decimal>(type.ToString(), roomGenerationSettingsDto)
            };
            if (GlobalLocalStorage.GetRoom(i) is null)
                Rooms.Add(newRoom);
        }
    }

    public void AddHoursToStorageTime(int countHours) => GlobalLocalStorage.AddHoursForTest += countHours;

    public IEnumerable<ApplicationViewModel> GenerateApplications(int countApplication, Interval<int> periodReversed)
    {
        var result = new List<ApplicationViewModel>();

        for (var i = 0; i < countApplication; i++)
        {
            var roomType = (RoomType)_random.Next(0, Enum.GetNames(typeof(RoomType)).Length);

            result.Add(new ApplicationViewModel
            {
                Peoples = GeneratePeoples(_random.Next(1, roomType.GetMaxPeople())),
                EndData = DateTime.Now.AddDays(_random.Next(periodReversed.Min, periodReversed.Max)).AddHours(_random.Next(1, 24)),
                Type = roomType,
            });
        }

        return result;
    }
}