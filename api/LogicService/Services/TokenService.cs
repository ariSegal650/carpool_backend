using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LogicService.Services
{
    public class TokenService
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration iconfiguration)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(iconfiguration["TokenKey"]));
        }


        public string GenerateJwtToken(string CompanyId,string adminPhone, string role)
        {
           

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new List<Claim>
                {
                  new Claim(ClaimTypes.NameIdentifier,CompanyId),
                  new Claim(ClaimTypes.MobilePhone, adminPhone),
                  new Claim(ClaimTypes.Role, role),
                }),
                Expires = DateTime.Now.AddDays(15), // Set token expiration time
                SigningCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var response=  tokenHandler.WriteToken(token);
            Console.WriteLine($"Generated Token: {response}");

            return response;
        }

    }
}