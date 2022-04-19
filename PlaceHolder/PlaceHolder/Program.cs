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

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
