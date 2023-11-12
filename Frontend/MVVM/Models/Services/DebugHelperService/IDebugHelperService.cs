using DataContract;
using DataContract.DTO.Settings;
using DataContract.DTO.ViewModels;

namespace HotelManager.MVVM.Models.Services.DebugHelperService;

public interface IDebugHelperService
{
    public void AddHoursToStorageTime(int countHours);
    public void GenerateTestRooms(RoomGenerationSettingsDto roomGenerationSettings);
    public IEnumerable<ApplicationViewModel> GenerateApplications(int maxCountApplication, Interval<int> periodReversed);

}