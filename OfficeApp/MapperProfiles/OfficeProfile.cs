using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OfficeApp.Dto;
using OfficeApp.Models;

namespace OfficeApp.MapperProfiles
{
    public class OfficeProfile : Profile
    {
        public OfficeProfile()
        {
            CreateMap<Office, OfficeDtoGet>();
            CreateMap<OfficeDtoPost, Office>();
            CreateMap<OfficeDtoPut, Office>();
        }
    }
}
