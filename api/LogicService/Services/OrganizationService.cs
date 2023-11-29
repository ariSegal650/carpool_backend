using AutoMapper;
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
        private readonly IMapper _mapper;

        public OrganizationService(DataContexst dataContexst, IMapper mapper)
        {
            _DataContexst = dataContexst;
            _mapper = mapper;
        }

        public async Task<bool> CreateOrganization(OrganizationDto org)
        {
            try
            {
                org.Name = System.Text.RegularExpressions.Regex.Replace(org.Name, @"\s+", " ").ToLower();
                await _DataContexst._Organization.InsertOneAsync(org.convertToEo());
                return true;
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync("OrganizationService" + e);
                return false;
            }
        }

        public async Task<OrganizationInfoEO?> GetOrganization(string adminPhone, string nameOrg)
        {
            
             adminPhone = System.Text.RegularExpressions.Regex.Replace(adminPhone, @"\s+", " ");
             nameOrg=  System.Text.RegularExpressions.Regex.Replace(nameOrg, @"\s+", " ").ToLower();
             var filter = Builders<OrganizationInfoEO>.Filter.And(
             Builders<OrganizationInfoEO>.Filter.Eq(org => org.Name, nameOrg),
             Builders<OrganizationInfoEO>.Filter.ElemMatch(org => org.Admins, admin => admin.Phone == adminPhone)
             );

            var organization = await _DataContexst._Organization.Find(filter).FirstOrDefaultAsync();

            return organization;
        }

        public async Task<OrganizationAdmin?> GetAdmin(OrganizationInfoEO org, string adminPhone)
        {

            if (org?.Admins == null) return null;
            adminPhone = System.Text.RegularExpressions.Regex.Replace(adminPhone, @"\s+", " ");
            var admin = await Task.Run(() => org.Admins.FirstOrDefault(adm => adm.Phone == adminPhone));

            if (admin == null) return null;
            return admin;
        }

        public async Task<List<OrganizationInfoEO>> GetAsync()
        {
            return await _DataContexst._Organization.Find(new BsonDocument()).ToListAsync();
        }
    }
}
