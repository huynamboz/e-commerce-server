using e_commerce_server.src.Core.Database.Data;
using e_commerce_server.src.Core.Env;
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


var configuration = new ConfigurationBuilder().AddJsonFile($"appsettings.json");

var config = configuration.Build();

var connectionString = config.GetConnectionString("ConnectionString");

builder.Services.AddDbContext<MyDbContext>(option=>
{
    option.UseSqlServer(connectionString);
});

builder.Services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
{
    builder.WithOrigins("http://192.168.91.1:3000").AllowAnyMethod().AllowAnyHeader();
}));

var secrectKey = ENV.JWT_SECRET;

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
