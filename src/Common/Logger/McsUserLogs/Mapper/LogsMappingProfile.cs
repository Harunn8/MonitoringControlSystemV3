using AutoMapper;
using McsUserLogs.Models;
using McsUserLogs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McsUserLogs.Mapper
{
    public class LogsMappingProfile : Profile
    {
        public LogsMappingProfile()
        {
            CreateMap<McsUserLogModel, McsUserLogResponse>().ReverseMap();
        }
    }
}
