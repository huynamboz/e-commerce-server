using e_commerce_server.src.Core.Env;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_server.src.Core.Database
{
    public class DatabaseConnect 
    {
        private readonly string _connectionString;
        private readonly string _environment;
        private WebApplicationBuilder _builder;
        private DatabaseConnect(WebApplicationBuilder builder)
        {
            _connectionString = ENV.CONNECTION_STRING;
            _environment = ENV.ENVIRONMENT;
            _builder = builder;
        }
        public static DatabaseConnect ApplyBuilderContext(WebApplicationBuilder builder)
        {
            return new DatabaseConnect(builder);
        }
        public WebApplicationBuilder ApplyDbContext()
        {
            this._builder.Services.AddDbContext<AppDbContext>(option => {
                option.UseSqlServer(this._connectionString);
            });

            if (this._environment == "production")
            {
                ApplyMigration();
            }

            return this._builder;
        }
        public void ApplyMigration()
        {
            this._builder.Services.BuildServiceProvider().GetService<AppDbContext>()?.Database.Migrate();
        }
    }
}
