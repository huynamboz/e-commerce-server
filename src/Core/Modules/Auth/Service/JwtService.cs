using e_commerce_server.src.Core.Database.Data;
using e_commerce_server.src.Core.Env;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace e_commerce_server.src.Core.Modules.Auth.Service
{
    public class JwtService
    {
        private readonly string _secret;
        private readonly double _expireIn;
        public JwtService() {
            _secret = ENV.JWT_SECRET;
            _expireIn = Convert.ToDouble(ENV.EXPIRE_DAY);
        }
        public string Sign(UserData user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secrectkeybytes = Encoding.UTF8.GetBytes(_secret);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, user.email),
                    new Claim("id", user.id.ToString()),
                    new Claim("role_id", user.role_id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(_expireIn),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secrectkeybytes), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = jwtTokenHandler.CreateToken(tokenDescription);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
