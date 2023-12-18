using AutoMapper;
using LogicService.Data;
using LogicService.Dto;
using LogicService.EO;
using Microsoft.AspNetCore.Authorization;
using MongoDB.Bson;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Text.RegularExpressions;

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

        public async Task<SimpelResponse> CreateOrganization(OrganizationDto org)
        {
            try
            {
                org.Name = Regex.Replace(org.Name, @"\s+", " ").Trim().ToLower();

                var filter = Builders<OrganizationInfoEO>.Filter.Eq(o => o.Name, org.Name);
                var exists = _DataContexst._Organization.Find(filter).Any();

                if (exists)
                {
                    return new(false, "השם ארגון כבר קיים , אנא בחר שם אחר ");
                }

                var mapped = _mapper.Map<OrganizationInfoEO>(org);
                mapped.Admins.Add(_mapper.Map<OrganizationAdmin>(org.admin));
                await _DataContexst._Organization.InsertOneAsync(mapped);

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
            nameOrg = Regex.Replace(nameOrg, @"\s+", " ").Trim().ToLower();

            var filter = Builders<OrganizationInfoEO>.Filter.And(
             Builders<OrganizationInfoEO>.Filter.Eq(org => org.Name, nameOrg),
             Builders<OrganizationInfoEO>.Filter.ElemMatch(org => org.Admins, admin => admin.Phone == adminPhone));

            var organization = await _DataContexst._Organization.Find(filter).FirstOrDefaultAsync();

            return organization;
        }

        public async Task<OrganizationInfoEO?> GetOrganizationById(string? adminPhone, string? organizationId)
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

        public async Task<OrganizationDto?> GetOrganizationById(string? jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(jwt) as JwtSecurityToken;
            var organizationId = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "nameid")?.Value;
            var admin_phone = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "MobilePhone")?.Value;

            var organization = await GetOrganizationById(admin_phone, organizationId);

            if (organization == null) return null;

            var mapped = _mapper.Map<OrganizationDto>(organization);
            mapped.admin = _mapper.Map<OrganizationAdminDto>(organization.Admins.First());

            return mapped;
        }

        public async Task<SimpelResponse> UpdateOrganization(string? jwt, OrganizationDto Neworg)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(jwt) as JwtSecurityToken;
            var organizationId = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "nameid")?.Value;
            var adminPhone = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "MobilePhone")?.Value;

            if (organizationId == null || adminPhone == null) return new(false, "שגיאה נוצרה");


            var existingOrganization = await GetOrganizationById(adminPhone, organizationId);
            if (existingOrganization == null) return new(false, "שגיאה נוצרה");

            if (Neworg.Logo != existingOrganization.Logo)
                existingOrganization.Logo = Neworg.Logo;
            if (Neworg.Phone != existingOrganization.Phone)
                existingOrganization.Phone = Neworg.Phone;
            if (Neworg.Email != existingOrganization.Email)
                existingOrganization.Email = Neworg.Email;
            if (Neworg.Website != existingOrganization.Website)
                existingOrganization.Website = Neworg.Website;

            if (!existingOrganization.Admins.Any()) { return new(false, "שגיאה נוצרה"); }

            if (Neworg.admin.Phone != existingOrganization.Admins.First().Phone)
                existingOrganization.Admins[0].Phone = Neworg.admin.Phone;
            if(Neworg.admin.Email!= existingOrganization.Admins.First().Email)
                existingOrganization.Admins[0].Email = Neworg.admin.Email;
            if(Neworg.admin.Name != existingOrganization.Admins.First().Name)
                existingOrganization.Admins[0].Name = Neworg.admin.Name;

            try
            {
                var filter = Builders<OrganizationInfoEO>.Filter.And(
                Builders<OrganizationInfoEO>.Filter.Eq(org => org.Id, organizationId));

                _DataContexst._Organization.ReplaceOne(filter, existingOrganization);
                return new(true, "העמותה עודכנה בהצלחה");
            }
            catch (Exception)
            {

            }
            return new(false, "שגיאה נוצרה");
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
