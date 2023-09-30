using AutoMapper;
using DataContract.BusinessModels;
using DataContract.ViewModelsDto;

namespace DataContract;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Room, RoomViewModel>().ReverseMap();
        CreateMap<People, PeopleViewModel>().ReverseMap();
        CreateMap<Reservation, ReservationViewModel>().ReverseMap();
    }
}

