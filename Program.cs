using e_commerce_server.Data;
using e_commerce_server.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var configuration = new ConfigurationBuilder()
     .AddJsonFile($"appsettings.json");

var config = configuration.Build();
var connectionString = config.GetConnectionString("ConnectionString");
builder.Services.AddDbContext<MyDbContext>(option=>
{
    option.UseSqlServer(connectionString);
});


//builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
//policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));




var settings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();

builder.Services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
{
    builder.WithOrigins("http://192.168.91.1:3000","https://hinam.one", "https://deploy--capable-taiyaki-728941.netlify.app").AllowAnyMethod().AllowAnyHeader();
}));


var secrectKey = settings.SecrectKey;
var secrectkeybytes = Encoding.UTF8.GetBytes(secrectKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secrectkeybytes),
            ClockSkew = TimeSpan.Zero
        };
    });



var app = builder.Build();
app.UseHttpsRedirection();
app.UseCors("ApiCorsPolicy");

// 
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "uploads")),
    RequestPath = new PathString("/uploads")
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
