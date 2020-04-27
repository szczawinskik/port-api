using ApplicationCore.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.MappingProfiles
{
    public class ScheduleProfile : Profile
    {
        public ScheduleProfile()
        {
            CreateMap<ScheduleViewModel, Schedule>()
                .ForMember(dest => dest.Arrival, opt => opt.MapFrom(src =>
                    new DateTime(src.Arrival.Year, src.Arrival.Month, src.Arrival.Day,
                    src.Arrival.Hour, src.Arrival.Minute, 0, DateTimeKind.Utc).ToLocalTime()))
                 .ForMember(dest => dest.Departure, opt => opt.MapFrom(src =>
                    new DateTime(src.Departure.Year, src.Departure.Month, src.Departure.Day,
                    src.Departure.Hour, src.Departure.Minute, 0, DateTimeKind.Utc).ToLocalTime()))
                 .ForMember(x => x.Ship, opt => opt.Ignore())
                 .ForMember(x => x.ShipId, opt => opt.Ignore())
                 .ForMember(x => x.DepartureSent, opt => opt.Ignore())
                 .ForMember(x => x.DepartureSent, opt => opt.Ignore())
                 .ForMember(x => x.ArrivalSent, opt => opt.Ignore());

            CreateMap<Schedule, ScheduleViewModel>();
        }
    }
}
