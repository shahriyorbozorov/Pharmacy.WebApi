using Microsoft.IdentityModel.Tokens;
using Pharmacy.WebApi.Interfaces.Managers;
using Pharmacy.WebApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pharmacy.WebApi.Services
{
    public class AuthManager : IAuthManager
    {
        private readonly IConfigurationSection _config;
        public AuthManager(IConfiguration configuration)
        {
            _config = configuration.GetSection("Jwt");
        }
        public string GeneratedToken(User user)
        {
            var claims = new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };

            var secretKey = _config["Key"];
            var issuer = _config["Issuer"];
            var audience = _config["Audience"];
            var expire = double.Parse(_config["Lifetime"]);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(issuer, audience, claims,
                expires: DateTime.Now.AddMinutes(expire),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
