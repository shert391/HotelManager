using DataContract.ViewModelsDto;

namespace HotelManager.MVVM.Models.Services.DebugHelperService;

public interface IDebugHelperService
{
    public void GenerateTestRooms();
    public void AddHoursToStorageTime(int countHours);
    public IEnumerable<ApplicationViewModel> GenerateApplications(int countApplication);
}