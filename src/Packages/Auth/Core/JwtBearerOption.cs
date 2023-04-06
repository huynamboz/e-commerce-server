using e_commerce_server.Src.Core.Env;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace e_commerce_server.Src.Packages.Auth.Core
{
    public class JwtBearerOption
    {
        private readonly string _secretKey;
        private readonly byte[] _secretKeyBytes;

        private JwtBearerOption()
        {
            this._secretKey = ENV.JWT_SECRET;
            this._secretKeyBytes = Encoding.UTF8.GetBytes(_secretKey);
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
                    IssuerSigningKey = new SymmetricSecurityKey(this._secretKeyBytes),
                    ClockSkew = TimeSpan.Zero
                };
            };
        }
    }
}
