using AutoMapper;
using McsCore.Entities;
using MonitorApplication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorApplication.Mapper
{
    public class MonitorMapper : Profile
    {
        public MonitorMapper()
        {
            CreateMap<ParameterLogs, ParameterLogTableResponse>().ReverseMap();
        }
    }
}
