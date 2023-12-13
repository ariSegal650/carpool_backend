using AutoMapper;
using LogicService.Data;
using LogicService.Dto;
using LogicService.EO;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;

namespace LogicService.Services
{
    public class RequstService
    {
        private readonly DataContexst _DataContexst;
        private readonly IMapper _mapper;
        private readonly OrganizationService _OrganizationService;

        public RequstService(DataContexst dataContexst, IMapper mapper, OrganizationService organizationService)
        {
            _DataContexst = dataContexst;
            _mapper = mapper;
            _OrganizationService = organizationService;
        }

        public async Task<SimpelResponse> AddReuqst(RequstDto request, string jwt)
        {
            try
            {
                var mapped = _mapper.Map<Request>(request);

                //get organizationId from jwt 
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(jwt) as JwtSecurityToken;
                var organizationId = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "nameid")?.Value;
                var admin_phone = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "MobilePhone")?.Value;

                // mapped.Id_Org = organizationId ?? "";

                if (admin_phone == null || organizationId == null) return new(false, "אירעה שגיעה");

                var Organization = await _OrganizationService.GetOrganizationById(admin_phone, organizationId);
                if (Organization == null) return new(false, "אירעה שגיעה");
                mapped.Organization = _mapper.Map<OrganizationDto>(Organization);

                var admin = await _OrganizationService.GetAdmin(Organization, admin_phone);
                if (admin == null) return new(false, "אירעה שגיעה");
                mapped.Organization.admin = _mapper.Map<OrganizationAdminDto>(admin);

                await _DataContexst._requsts.InsertOneAsync(mapped);
                return new(true, "הבקשה נוספה בהצלחה");
            }
            catch (Exception)
            {
                return new(false, "אירעה שגיעה");
            }
        }

        public async Task<List<Request>?> GetAllRequest(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(jwt) as JwtSecurityToken;
            var organizationId = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "nameid")?.Value;

            // Check if organizationId is not null
            if (organizationId == null)
            {
                return null;
            }

            var filter = Builders<Request>.Filter.Eq(req => req.Organization!.Id, organizationId);
            var request = await _DataContexst._requsts.Find(filter).ToListAsync();

            return request;
        }
    }
}
