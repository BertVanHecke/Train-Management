using AutoMapper;
using TreinRittenApplicatie_VanHeckeBert.API.Models;
using TreinRittenApplicatie_VanHeckeBert.API.ViewModels;
using TreinRittenApplicatie_VanHeckeBert.Domain.Entities;

namespace TreinRittenApplicatie_VanHeckeBert.API.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Station, StationViewModel>();
            CreateMap<Ride, TrainViewModel>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Train.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Train.Name))
                .ForMember(dest => dest.Capacity, opts => opts.MapFrom(src => src.Train.Capacity));
            CreateMap<ApplicationUser, UserViewModel>();
        }
    }
}
