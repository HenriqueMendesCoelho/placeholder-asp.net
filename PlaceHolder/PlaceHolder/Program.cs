global using PlaceHolder.Data;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Mvc;
global using PlaceHolder.Models;
global using PlaceHolder.Services.Implamentations;
global using PlaceHolder.Services;
global using PlaceHolder.Repositories.Implamentations;
global using PlaceHolder.Repositories;
global using System.Text.Json.Serialization;
global using System.ComponentModel.DataAnnotations;
using PlaceHolder.Repositories.Generic;
using PlaceHolder.Security;
using PlaceHolder.Security.Implementations;
using PlaceHolder.Configurations;
using Microsoft.Extensions.Options;
using System.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

//Dependency Injection
builder.Services.AddScoped<IUserService, UserServiceImplementation>();
builder.Services.AddScoped<IUserRepository, UserRepositoryImplementation>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ITicketRepository, TicketRepositoryImplementation>();
builder.Services.AddScoped<ITicketService, TicketServiceImplementation>();
builder.Services.AddScoped<IHistoricService, HistoricServiceImplementation>();
builder.Services.AddScoped<IUserAddressService, UserAddressImplementation>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthServiceImplementation>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var token = builder.Configuration.GetSection("TokenConfiguratios").Get<TokenConfiguration>();

builder.Services.AddSingleton(token);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = token.Issuer,
        ValidAudience = token.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token.Secret))
    };
});

builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser().Build());
});


#region [DBContext]
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
#endregion

#region [Cors]
builder.Services.AddCors();
#endregion

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region [CorsApp]
app.UseCors( c =>
{
    c.AllowAnyOrigin();
    c.WithMethods("GET", "PATH", "DELETE", "POST");
    c.AllowAnyHeader();
});
#endregion

//app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
