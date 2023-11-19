﻿using LogicService.Data;
using LogicService.Dto;
using LogicService.EO;
using MongoDB.Bson;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;

namespace LogicService.Services
{
    public class OrganizationService
    {
        private readonly DataContexst _DataContexst;
        public OrganizationService(DataContexst dataContexst)
        {
            _DataContexst = dataContexst;
        }
        public async Task<bool> CreateOrganization(OrganizationDto org)
        {
            try
            {
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

            var admin = await Task.Run(() => org.Admins.FirstOrDefault(adm => adm.Phone == adminPhone));

            if (admin == null) return null;
            return admin;
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

        public async Task<List<OrganizationInfoEO>> GetAsync()
        {
            return await _DataContexst._Organization.Find(new BsonDocument()).ToListAsync();
        }
    }
}
