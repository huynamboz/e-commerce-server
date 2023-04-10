using e_commerce_server.src.Core.Env;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_server.src.Core.Database
{
    public class DatabaseConnect 
    {
        private readonly string _connectionString;
        private readonly string _environment;
        private WebApplicationBuilder _builder;
        private DatabaseConnect()
        {
            this._connectionString = ENV.CONNECTION_STRING;
            this._environment = ENV.ENVIRONMENT;
        }
        public static DatabaseConnect Builder()
        {
            return new DatabaseConnect();
        }
        public DatabaseConnect ApplyBuilderContext(WebApplicationBuilder builder)
        {
            this._builder = builder;
            return this;
        }
        public WebApplicationBuilder ApplyDbContext()
        {
            this._builder.Services.AddDbContext<MyDbContext>(option => {
                option.UseSqlServer(this._connectionString);
            });

            // if (this._environment == "production")
            // {
                ApplyMigration();
            // }

            return this._builder;
        }
        public void ApplyMigration()
        {
            this._builder.Services.BuildServiceProvider().GetService<MyDbContext>().Database.Migrate();
        }
    }
}
