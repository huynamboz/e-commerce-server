using e_commerce_server.src.Core.Env;

namespace e_commerce_server.src.Packages.Extensions.Cors
{
    public class CorsOption
    {
        private readonly string _client;

        private CorsOption()
        {
            this._client = ENV.CLIENT;
        }
        public static CorsOption Builder()
        {
            return new CorsOption();
        }
        public Action<Microsoft.AspNetCore.Cors.Infrastructure.CorsOptions> ConfigureSetupAction()
        {
            return options =>
                options.AddPolicy("ApiCorsPolicy", this.ConfigurePolicy());
        }
        public Action<Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicyBuilder> ConfigurePolicy() {
            return builder =>
                builder
                    .WithOrigins(this._client)
                    .AllowAnyMethod()
                    .AllowAnyHeader();
        }
    }
}
