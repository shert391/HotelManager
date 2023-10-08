using AutoMapper;
using DataContract.BusinessModels;
using DataContract.ViewModelsDto;
using DataContract.ViewModelsDto.Messages;

namespace DataContract;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Room, Room>();
        CreateMap<People, People>();
        CreateMap<Reservation, Reservation>();
        CreateMap<PayInformation, PayInformation>();
        
        CreateMap<People, PeopleViewModel>().ReverseMap();
        CreateMap<Reservation, ReservationViewModel>().ReverseMap();
        CreateMap<PayInformation, PayInformationDto>().ReverseMap();
        CreateMap<Room, RoomViewModel>().ForMember(dst => dst.Score, opt => opt.MapFrom(src => AverageScore(src.Feedbacks))).ReverseMap();
        CreateMap<Room, RoomViewModel>().ForMember(dst => dst.CurrentState, opt => opt.MapFrom(src => GetRoomType(src.Reservation))).ReverseMap();
    }

    /// <summary>
    /// Score у Room для RoomDTO будет сопоставлятся как среднее арифмитическое между всеми оценками у Room
    /// </summary>
    /// <returns></returns>
    private double AverageScore(IReadOnlyCollection<Assessment> assessments)
    {
        return assessments.Count == 0 ? 0 : assessments.Sum(x => x.Score) / assessments.Count;
    }

    /// <summary>
    /// Метод проецирования наличия брони комнаты на RoomType соответствующего DTO
    /// </summary>
    /// <param name="reservation"></param>
    /// <returns></returns>
    private RoomState GetRoomType(Reservation? reservation)
    {
        return reservation is not null ? RoomState.Busy : RoomState.Free;
    }
}