using AutoMapper;
using DataContract.BusinessModels;
using DataContract.ViewModelsDto;
using DataContract.ViewModelsDto.Messages;

namespace DataContract;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<People, PeopleViewModel>().ReverseMap();
        CreateMap<Reservation, ReservationViewModel>().ReverseMap();
        CreateMap<PayInformation, PayInformationDto>().ReverseMap();
        CreateMap<Room, RoomViewModel>().ForMember(dst => dst.Score, opt => opt.MapFrom(src => AverageScore(src.Feedbacks))).ReverseMap();
    }

    /// <summary>
    /// Score у Room для RoomDTO будет сопоставлятся как среднее арифмитическое между всеми оценками у Room
    /// </summary>
    /// <returns></returns>
    private double AverageScore(IReadOnlyCollection<Assessment> assessments)
    {
        return assessments.Count == 0 ? 0 : assessments.Sum(x => x.Score) / assessments.Count;
    }
}