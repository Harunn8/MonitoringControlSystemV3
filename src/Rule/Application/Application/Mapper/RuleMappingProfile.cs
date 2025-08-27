using Application.Models;
using AutoMapper;
using RuleApplication.Models;
using RuleApplication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleApplication.Mapper
{
    public class RuleMappingProfile : Profile
    {
        public RuleMappingProfile()
        {
            CreateMap<AlarmModel,AlarmResponse>().ReverseMap();
            CreateMap<ScriptModel, ScriptResponse>().ReverseMap();
        }
    }
}
