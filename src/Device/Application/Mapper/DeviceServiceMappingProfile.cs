using Application.Responses;
using AutoMapper;
using DeviceApplication.Models;
using McsCore.Entities;

namespace Application.Mapper
{
    public class DeviceServiceMappingProfile : Profile
    {
        public DeviceServiceMappingProfile()
        {
            CreateMap<SnmpDeviceModel, SnmpDeviceResponses>()
                .ReverseMap();

            CreateMap<BaseDeviceModel,SnmpDeviceModel>()
                .ReverseMap();

            CreateMap<SnmpDeviceModel,SnmpDeviceResponses>()
                .ReverseMap();
        }
    }
}
