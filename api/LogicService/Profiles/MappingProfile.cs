using AutoMapper;
using LogicService.Dto;
using LogicService.EO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicService.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           
            CreateMap<Request, RequstDto>().ReverseMap();
            CreateMap<OrganizationAdmin, OrganizationAdminDto>().ReverseMap();
        }
    }
}
