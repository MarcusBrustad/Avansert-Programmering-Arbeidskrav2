using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using TodoApi.Auth;
using TodoApi.Data;
using TodoApi.DTOs.Users;
using TodoApi.Extensions;
using TodoApi.Mappers;
using TodoApi.Models;
using TodoApi.Services.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterMappers();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddDbContext<TodoApiDbContext>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.Run();