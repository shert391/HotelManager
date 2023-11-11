using DataContract.ViewModelsDto;

namespace HotelManager.MVVM.Models.Services.DebugHelperService;

public interface IDebugHelperService
{
    public void AddHoursToStorageTime(int countHours);
    public void GenerateTestRooms(RoomGenerationSettingsDto roomGenerationSettings);
    public IEnumerable<ApplicationDto> GenerateApplications(int maxCountApplication, int maxPeriodReserved);

}