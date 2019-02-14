using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OfficeApp.Dto;
using OfficeApp.Models;

namespace OfficeApp.MapperProfiles
{
    public class UsageProfile : Profile
    {
        public UsageProfile()
        {
            CreateMap<Usage, UsageDtoGet>()
                .ForMember(d => d.Device, s => s.MapFrom(x => x.Device.Name))
                .ForMember(d => d.Person, s => s.MapFrom(x => x.Person.FirstName + " " + x.Person.LastName));

            CreateMap<Usage, UsageTimeDtoGet>()
                .ForMember(d => d.UsedFrom, s => s.MapFrom(x => x.UsedFrom))
                .ForMember(d => d.UsedTo, s => s.MapFrom(x => x.UsedTo));

            CreateMap<Usage, UsageAllByPersonDtoGet>()
                .ForMember(d => d.Device, s => s.MapFrom(x => x.Device.Name));

            CreateMap<Usage, TimeUsedByPersonDtoGet>()
                .ForMember(d => d.Device, s => s.MapFrom(x => x.Device.Name))
                .ForMember(d => d.TimeUsed, s => s.MapFrom(x => x.UsedFrom));
        }
    }
}
