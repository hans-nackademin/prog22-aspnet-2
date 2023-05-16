using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Models.Entities;

namespace WebApi.Helpers.Services
{
    public class TokenGenerator
    {
        private readonly IConfiguration _configuration;

        public TokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(UserEntity user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()!),
                new Claim(ClaimTypes.GivenName, user.FirstName!),
                new Claim(ClaimTypes.Surname, user.LastName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Name, user.Email!),
                new Claim(ClaimTypes.Role, "user"),
                new Claim("DisplayName", $"{user.FirstName} {user.LastName}")
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["Token:Issuer"],
                Audience = _configuration["Token:Audience"],
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Secret"]!)), SecurityAlgorithms.HmacSha512Signature),
                Subject = new ClaimsIdentity(claims)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(securityTokenDescriptor));
        }
    }
}
