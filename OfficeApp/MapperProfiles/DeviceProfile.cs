using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OfficeApp.Dto;
using OfficeApp.Models;

namespace OfficeApp.MapperProfiles
{
    public class DeviceProfile : Profile
    {
        public DeviceProfile()
        {
            CreateMap<Device, DeviceDtoGet>();
            CreateMap<DeviceDtoPost, Device>();
            CreateMap<DeviceDtoPut, Device>();
        }
    }
}
