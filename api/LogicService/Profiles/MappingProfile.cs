using AutoMapper;
using LogicService.Dto;
using LogicService.EO;


namespace LogicService.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           
            CreateMap<Request, RequstDto>().ReverseMap();
            CreateMap<OrganizationAdmin, OrganizationAdminDto>().ReverseMap();

            CreateMap<OrganizationDto, OrganizationInfoEO>().ForMember(dest => dest.Secret, opt => opt.Ignore()).ReverseMap();
        }
    }
}
