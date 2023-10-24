using AutoMapper;
using TreinRittenApplicatie_VanHeckeBert.Domain.Entities;
using TreinRittenApplicatie_VanHeckeBert.ViewModels;

namespace TreinRittenApplicatie_VanHeckeBert.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Station, StationViewModel>();
            CreateMap<StationViewModel, Station>();
            CreateMap<Train, TrainViewModel>();
            CreateMap<TrainViewModel, Train>();
            CreateMap<Ride, RideViewModel>()
                .ForMember(dest => dest.FromStationCity, opts => opts.MapFrom(src => src.FromStation.City))
                .ForMember(dest => dest.ToStationCity, opts => opts.MapFrom(src => src.ToStation.City))
                .ForMember(dest => dest.TrainName, opts => opts.MapFrom(src => src.Train.Name))
                .ForMember(dest => dest.TrainCapacity, opts => opts.MapFrom(src => src.Train.Capacity));
            CreateMap<RideViewModel, Ride>();
            CreateMap<Ticket, TicketViewModel>()
                .ForMember(dest => dest.Rides, opts => opts.MapFrom(src => src.Rides));
            CreateMap<TicketViewModel, Ticket>();
            CreateMap<BookingTicket, BookingTicketViewModel>();
            CreateMap<Booking, BookingViewModel>()
                .ForMember(dest => dest.AspNetUserFirstName, opts => opts.MapFrom(src => src.AspNetUser.FirstName))
                .ForMember(dest => dest.AspNetUserLastName, opts => opts.MapFrom(src => src.AspNetUser.LastName));
            CreateMap<Seat, SeatViewModel>();
        }
    }
}
