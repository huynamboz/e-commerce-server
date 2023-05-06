using e_commerce_server.src.Core.Config;
using e_commerce_server.src.Packages.Swagger.Filter;

namespace e_commerce_server.src.Packages.Swagger.Core
{
    public class SwaggerBuilder
    {
        private WebApplicationBuilder _builder;
        private SwaggerBuilder(WebApplicationBuilder builder)
        {
            this._builder = builder;
        }
        public static SwaggerBuilder ApplyBuilderContext(WebApplicationBuilder builder)
        {
            return new SwaggerBuilder(builder);
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
                options.AddSecurityDefinition(SwaggerConfig.ApiSecurityScheme().Reference.Id, SwaggerConfig.ApiSecurityScheme());

                //Make sure Swagger UI requires the Authorization header to be set
                options.OperationFilter<AuthorizeCheckOperationFilter>();
            });
            return _builder;
        }
    }
}
