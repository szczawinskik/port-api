using ApplicationCore.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.MappingProfiles
{
    public class ShipProfile : Profile
    {
        public ShipProfile()
        {

            CreateMap<Ship, ShipAggregateViewModel>()
                .ForMember(x => x.ShipOwnerName, opt => opt.MapFrom(x => x.ShipOwner.Name))
                .ForMember(x => x.ClosestSchedule, opt => opt.MapFrom(x => x.ClosestSchedule));

            CreateMap<Ship, ShipViewModel>()
                .IncludeBase<Ship, ShipAggregateViewModel>()
                .ForMember(x => x.Schedules, opt => opt.MapFrom(x => x.Schedules));

            CreateMap<ShipViewModel, Ship>()
               .ForMember(x => x.ShipOwner, opt => opt.Ignore())
               .ForMember(x => x.Schedules, opt => opt.MapFrom(x => x.Schedules));

            //CreateMap<ShipViewModel, Ship>()
            //   .ForMember(x => x.ShipOwner, opt => opt.Ignore())
            //   .ForMember(x => x.Schedules, opt => opt.MapFrom(x => x.Schedules));
        }
    }
}
