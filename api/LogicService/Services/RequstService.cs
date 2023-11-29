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

        public RequstService(DataContexst dataContexst, IMapper mapper)
        {
            _DataContexst = dataContexst;
            _mapper = mapper;
        }

        public async Task<bool> AddReuqst(RequstDto request,string jwt)
        {
            try
            {
                var mapped = _mapper.Map<Request>(request);

                //get organizationId from jwt 
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(jwt) as JwtSecurityToken;
                var organizationId = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "nameid")?.Value;
                var admin_phone = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "MobilePhone")?.Value;

                mapped.Id_Org = organizationId ?? "";
                mapped.Admin_Phone = admin_phone ?? "";

                await _DataContexst._requsts.InsertOneAsync(mapped);
                return true;
            }
            catch (Exception)
            {
                return false;
                //throw;
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

            var filter = Builders<Request>.Filter.Eq(req => req.Id_Org, organizationId);
            var request = await _DataContexst._requsts.Find(filter).ToListAsync();

            return request;

        }
    }
}
