using e_commerce_server.Src.Core.Database.Data;
using e_commerce_server.Src.Core.Env;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_server.Src.Core.Database;

public class DatabaseConnect {
    private readonly string _connectionString;
    private WebApplicationBuilder _builder;
    private DatabaseConnect() {
        this._connectionString = ENV.CONNECTION_STRING;
    }
    public static DatabaseConnect Builder() {
        return new DatabaseConnect();
    }
    public DatabaseConnect ApplyBuilderC(WebApplicationBuilder builder) {
        this._builder = builder;
        return this;
    }
    public DatabaseConnect ApplyDbContext() {
        this._builder.Services.AddDbContext<MyDbContext>(option => {
            option.UseSqlServer(this._connectionString);
        });
        return this;
    }
    public WebApplicationBuilder ReturnBuilder() {
        return this._builder;
    }
}
