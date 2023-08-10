using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Security.Claims;
using System.Text;

namespace ConnectFourWeb.Data
{
    public class JwtTokenProvider
    {
        public JwtTokenProvider(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public string GenerateJwtToken(string username)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            var key = GenerateKey(Configuration["AuthSettings:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private byte[] GenerateKey(string key)
        {
            return SHA256.HashData(Encoding.UTF8.GetBytes(key));
        }
    }
}
