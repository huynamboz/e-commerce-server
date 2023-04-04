using e_commerce_server.Src.Core.Config;
using e_commerce_server.Src.Packages.Swagger.Filter;

namespace e_commerce_server.Src.Packages.Swagger.Core
{
    public class SwaggerBuilder
    {
        private WebApplicationBuilder _builder;
        public static SwaggerBuilder Builder()
        {
            Console.WriteLine("SwaggerBuilder is bundling");
            return new SwaggerBuilder();
        }

        public SwaggerBuilder ApplyBuilderContext(WebApplicationBuilder builder)
        {
            this._builder = builder;
            return this;
        }
        public SwaggerBuilder ApplyEndPoint()
        {
            this._builder.Services.AddEndpointsApiExplorer();
            return this;
        }
        public WebApplicationBuilder ApplyConfig()
        {
            this._builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", SwaggerConfig.ApplyInfo());

                // Configure Swagger to use the Authorization header
                options.AddSecurityDefinition("Bearer", SwaggerConfig.ApiSecurityScheme());

                //Make sure Swagger UI requires the Authorization header to be set
                options.OperationFilter<AuthorizeCheckOperationFilter>();
            });
            return _builder;
        }
    }
}
