﻿using e_commerce_server.src.Core.Database.Data;
using e_commerce_server.src.Core.Env;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace e_commerce_server.src.Core.Modules.Auth.Service
{
    public class JwtService
    {
        private readonly string _secret;
        private readonly double _expireDay;
        private readonly double _expireMinute;
        public JwtService()
        {
            _secret = ENV.JWT_SECRET;
            _expireMinute = Convert.ToDouble(ENV.EXPIRE_MINUTE);
            _expireDay = Convert.ToDouble(ENV.EXPIRE_DAY);
        }
        public string GenerateAccessToken(UserData user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(_secret);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.email),
                new Claim("id", user.id.ToString()),
                new Claim(ClaimTypes.Role, user.role_id.ToString()),
                new Claim("role_id", user.role_id.ToString()),
            };

            var SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_expireMinute),
                signingCredentials: SigningCredentials
            );

            var jwt = jwtTokenHandler.WriteToken(token);

            return jwt;
        }

        public string GenerateRefreshToken(UserData user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(_secret);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(_expireDay),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = jwtTokenHandler.CreateToken(tokenDescription);

            return jwtTokenHandler.WriteToken(token);
        }

        public ClaimsPrincipal? Verify(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(this._secret);

            var validationParameters = new TokenValidationParameters
            {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
                    ClockSkew = TimeSpan.Zero
            };

            try {
                return tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
