using DataContract.DTO;
using DataContract.DTO.MappingEntities;
using DataContract.DTO.ViewModels;

namespace HotelManager.MVVM.Models.Services.ApplicationService;

public interface IApplicationService
{
    /// <summary>
    /// Ищет лучшую комнату по критериям заявки по такому принципу:
    /// 1) Выбираем все не занятые комнаты соответствующего типа
    /// 2) Из этих выбираем первую комнату с минимальной разницей между: макс-вместимость комнаты - кол-во людей в заявке на заселение 
    /// </summary>
    /// <param name="application"></param>
    /// <returns></returns>
    public int FindBestRoom(ApplicationViewModel application);
    public bool Handle(ApplicationViewModel application);
}