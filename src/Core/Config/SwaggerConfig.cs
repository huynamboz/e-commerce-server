using Microsoft.OpenApi.Models;

namespace e_commerce_server.Src.Core.Config
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
                    Name = "John Smith",
                    Email = "john.smith@example.com"
                }
            };
        }

        public static OpenApiSecurityScheme ApiSecurityScheme()
        {
            return new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Enter your token",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            };
        }
    }
}
