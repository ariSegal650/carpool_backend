using LogicService.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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

        public string CreateToken(OrganizationDto org)
        {
            var claims = new List<Claim>
            {
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.NameId, org.Id ?? ""),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Name, org.Name),

            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescripter = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(15),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescripter);

            return tokenHandler.WriteToken(token);
        }
    }
}