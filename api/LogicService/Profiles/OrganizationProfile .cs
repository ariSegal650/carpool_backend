
using AutoMapper;
using LogicService.Dto;
using LogicService.EO;

namespace LogicService.Profiles
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<OrganizationInfoEO, OrganizationDto>()
                .ForMember(dest => dest.admin, opt => opt.MapFrom(src => src.Admins != null && src.Admins.Count > 0 ? src.Admins[0] : null));

            CreateMap<OrganizationDto, OrganizationInfoEO>()
                .ForMember(dest => dest.Admins, opt => opt.MapFrom(src => new List<OrganizationAdmin>{}));

            CreateMap<OrganizationAdminDto, OrganizationAdmin>().ReverseMap();
        }

    }
}