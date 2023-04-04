using e_commerce_server.Src.Core.Env;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace e_commerce_server.Src.Packages.Auth.Core
{
    public class JwtBearerOption
    {
        private readonly string _secrectKey;
        private readonly byte[] _secrectkeybytes;

        private JwtBearerOption()
        {
            this._secrectKey = ENV.JWT_SECRET;
            this._secrectkeybytes = Encoding.UTF8.GetBytes(_secrectKey);
        }
        public static JwtBearerOption Builder()
        {
            return new JwtBearerOption();
        }
        public Action<JwtBearerOptions> ConfigureJwtBearerOptions()
        {
            return options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(this._secrectkeybytes),
                    ClockSkew = TimeSpan.Zero
                };
            };
        }
    }
}
