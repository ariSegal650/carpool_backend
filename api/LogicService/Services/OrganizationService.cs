

using LogicService.Data;
using LogicService.Dto;
using LogicService.EO;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LogicService.Services
{
    public class OrganizationService
    {
        private readonly DataContexst _DataContexst;
        private readonly TokenService _tokenService;
        public OrganizationService(DataContexst dataContexst, TokenService tokenService)
        {
            _DataContexst = dataContexst;
            _tokenService = tokenService;
        }
        public async Task<OrgResponseDto?> CreateOrganization(OrganizationDto org)
        {
            try
            {
             await _DataContexst._Organization.InsertOneAsync(org.convertToEo());

                return new OrgResponseDto
                {
                    Id = org.Id ?? "",
                    Token = _tokenService.CreateToken(org)
                };
            }
            catch (Exception e)
            {
              await Console.Out.WriteLineAsync("OrganizationService"+e);
                return null;
            }
        }

        public async Task<List<OrganizationInfoEO>> GetAsync()
        {
            return await _DataContexst._Organization.Find(new BsonDocument()).ToListAsync();
        }
    }
}
