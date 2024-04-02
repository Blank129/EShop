using DataAccess.EShop.EntitiesFramework;
using DataAccess.EShop.IServices;
using DataAccess.EShop.Services;
using DataAccess.EShop.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EShopDBContext>(options =>
options.UseSqlServer(configuration.GetConnectionString("ConnStr")));// 'ConnStr' là ConnectionString trog file AppSetting

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],//giá trị này bên appconfig
        ValidAudience = builder.Configuration["Jwt:ValidAudience"],//giá trị này bên appconfig
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))//giá trị này bên appconfig
    };
});
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserFunctionRepository, UserFunctionRepository>();
builder.Services.AddTransient<IEShopUnitOfWord, EShopUnitOfWord>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
