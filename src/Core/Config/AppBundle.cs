﻿using e_commerce_server.src.Core.Database;
using e_commerce_server.src.Core.Env;
using e_commerce_server.src.Packages.Auth.Core;
using e_commerce_server.src.Packages.Extensions.Cors;
using e_commerce_server.src.Packages.Files.Core;
using e_commerce_server.src.Packages.Swagger.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace e_commerce_server.src.Core.Config
{
    public class AppBundle
    {
        private WebApplicationBuilder _builder;
        private readonly string _environment;
        private AppBundle(WebApplicationBuilder builder)
        {
            _environment = ENV.ENVIRONMENT;
            _builder = builder;
        }
        public static AppBundle ApplyBuilderContext(WebApplicationBuilder builder)
        {
            return new AppBundle(builder);
        }
        public AppBundle Init() 
        {
            _builder.Services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = 50 * 1024 * 1024; //50MB
            });
            return this;
        }
        public AppBundle ApplyControllers()
        {
            _builder.Services.AddControllers();
            return this;
        }
        public AppBundle ApplySwagger()
        {
            _builder = SwaggerBuilder
                .ApplyBuilderContext(_builder)
                .ApplyEndPoint()
                .ApplyConfig();
            return this;
        }
        public AppBundle ApplyDbContext()
        {
            _builder = DatabaseConnect
                .ApplyBuilderContext(_builder)
                .ApplyDbContext();
            return this;
        }
        public AppBundle AddCors()
        {
            _builder.Services
                .AddCors(
                    CorsOption
                        .Builder()
                        .ConfigureSetupAction()
                );
            return this;
        }
        public AppBundle ApplyAuth()
        {
            _builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(
                    JwtBearerOption
                        .Builder()
                        .ConfigureJwtBearerOptions()
                 );
            return this;
        }
        public void Run()
        {
            var app = _builder.Build();
            app.UseHttpsRedirection();
            app.UseCors("ApiCorsPolicy");

            if (_environment == "development")
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.HeadContent = "<link rel='stylesheet' type='text/css' href='/files/css/swagger-ui.css'>";
                });

            }

            app.UseStaticFiles(
                FilesOption
                    .Builder()
                    .ConfigureStaticFilesOptions()
            );

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
