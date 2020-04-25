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
            CreateMap<ShipViewModel, Ship>()
                .ForMember(x => x.ShipOwner, opt => opt.Ignore())
                .ForMember(x => x.Schedules, opt => opt.Ignore());

            CreateMap<Ship, ShipViewModel>();
        }
    }
}
