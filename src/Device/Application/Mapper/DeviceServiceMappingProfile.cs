using Application.Models;
using Application.Responses;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper
{
    public class DeviceServiceMappingProfile : Profile
    {
        public DeviceServiceMappingProfile()
        {
            CreateMap<SnmpDeviceModel, SnmpDeviceResponses>()
                .ReverseMap();

            CreateMap<TcpDeviceModel,TcpDeviceResponses>()
                .ReverseMap();
        }
    }
}
