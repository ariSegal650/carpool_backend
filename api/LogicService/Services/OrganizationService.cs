

using LogicService.Data;
using LogicService.EO;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LogicService.Services
{
    public class OrganizationService
    {
        private readonly DataContexst _DataContexst;
        public OrganizationService(DataContexst dataContexst)
        {
            _DataContexst = dataContexst;
        }
        public async Task<bool> CreateOrganization(OrganizationInfoEO org)
        {
            try
            {
             await _DataContexst._Organization.InsertOneAsync(org);
                return true;
            }
            catch (Exception e)
            {
              await Console.Out.WriteLineAsync("OrganizationService"+e);
                return false;
            }
           
        }

        public async Task<List<OrganizationInfoEO>> GetAsync()
        {
            return await _DataContexst._Organization.Find(new BsonDocument()).ToListAsync();
        }
    }
}
