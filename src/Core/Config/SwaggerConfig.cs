using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace e_commerce_server.src.Core.Config
{
    public class SwaggerConfig
    {
        public static OpenApiInfo ApplyInfo()
        {
            return new OpenApiInfo
            {
                Title = "E-commerce Server",
                Version = "1.0.0",
                Description = "API for an e-commerce server",
                Contact = new OpenApiContact
                {
                    Name = "Admin",
                    Email = "user@example.com"
                }
            };
        }
        public static OpenApiSecurityScheme ApiSecurityScheme()
        {
            return new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Enter your token",

                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
        }
    }
}
