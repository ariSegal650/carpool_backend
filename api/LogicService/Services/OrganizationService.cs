using AutoMapper;
using LogicService.Data;
using LogicService.Dto;
using LogicService.EO;
using Microsoft.AspNetCore.Authorization;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.RegularExpressions;

namespace LogicService.Services
{
    public class OrganizationService
    {
        private readonly DataContexst _DataContexst;

        public OrganizationService(DataContexst dataContexst)
        {
            _DataContexst = dataContexst;
        }

        public async Task<SimpelResponse> CreateOrganization(OrganizationDto org)
        {
            try
            {
                org.Name = Regex.Replace(org.Name, @"\s+", " ").Trim().ToLower();

                var Exists = _DataContexst._Organization.Find(o => o.Name == org.Name).Any();

                if(Exists)
                {
                    return new(false, "השם ארגון כבר קיים , אנא בחר שם אחר ");
                }

                await _DataContexst._Organization.InsertOneAsync(org.convertToEo());
                return new(true, "העמותה נוצרה בהצלחה"); ;
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync("OrganizationService" + e);
                return new(false, "שגיאה נוצרה"); ;
            }
        }

        public async Task<OrganizationInfoEO?> GetOrganization(string adminPhone, string nameOrg)
        {

            if (nameOrg == null || adminPhone == null) return null;

             adminPhone = Regex.Replace(adminPhone, @"\s+", " ");
             nameOrg = Regex.Replace(nameOrg, @"\s+", " ").ToLower();

             var filter = Builders<OrganizationInfoEO>.Filter.And(
             Builders<OrganizationInfoEO>.Filter.Eq(org => org.Name, nameOrg),
             Builders<OrganizationInfoEO>.Filter.ElemMatch(org => org.Admins, admin => admin.Phone == adminPhone));

            var organization = await _DataContexst._Organization.Find(filter).FirstOrDefaultAsync();

            return organization;
        }
        public async Task<OrganizationInfoEO?> GetOrganizationById(string adminPhone, string organizationId)
        {
            if (organizationId == null || adminPhone == null) return null;

            adminPhone = Regex.Replace(adminPhone, @"\s+", " ");

            var filter = Builders<OrganizationInfoEO>.Filter.And(
            Builders<OrganizationInfoEO>.Filter.Eq(org => org.Id, organizationId),
            Builders<OrganizationInfoEO>.Filter.ElemMatch(org => org.Admins, admin => admin.Phone == adminPhone)
            );

            var organization = await _DataContexst._Organization.Find(filter).FirstOrDefaultAsync();

            return organization;
        }

        public async Task<OrganizationAdmin?> GetAdmin(OrganizationInfoEO org, string adminPhone)
        {
            if (org?.Admins == null || adminPhone == null) return null;

            adminPhone = Regex.Replace(adminPhone, @"\s+", " ");
            var admin = await Task.Run(() => org.Admins.FirstOrDefault(adm => adm.Phone == adminPhone));

            if (admin == null) return null;
            return admin;
        }

        [Authorize]
        public async Task<List<OrganizationInfoEO>> GetAsync()
        {
            return await _DataContexst._Organization.Find(new BsonDocument()).ToListAsync();
        }
    }
}
