using CityInfo;
using CityInfo.DbContexts;
using CityInfo.Repositories;
using CityInfo.Services;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

#if DEBUG
Log.Logger = new LoggerConfiguration()
.MinimumLevel.Debug()
.WriteTo.Console()
.CreateLogger();
#else
Log.Logger = new LoggerConfiguration()
.MinimumLevel.Debug()
.WriteTo.Console()
.WriteTo.File("logging/cityinfo.txt", rollingInterval: RollingInterval.Day)
.CreateLogger(); 
#endif

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
})
    .AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();
builder.Services.AddSingleton<CitiesDataStore>();

#if DEBUG
builder.Services.AddScoped<IMailService, MailService>();
#else
builder.Services.AddScoped<IMailService, IntegratedMailService>();
#endif

builder.Services.AddDbContext<CityInfoDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DbConnection"));
});

builder.Services.AddScoped<ICityInfoRepository, CityInfoRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
        };
    }
);

var app = builder.Build();

#region PipeLine

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

#endregion

app.Run();
