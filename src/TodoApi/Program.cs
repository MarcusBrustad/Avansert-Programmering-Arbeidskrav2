using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;
using TodoApi.Auth;
using TodoApi.Data;
using TodoApi.DTOs.Users;
using TodoApi.Extensions;
using TodoApi.Mappers;
using TodoApi.Middleware;
using TodoApi.Models;
using TodoApi.Repositories.Users;
using TodoApi.Services.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddSerilog();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddMappers();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddRepositories();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthHandler>(
        "BasicAuthentication", options => { });

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Components ??= new();
        document.Components.SecuritySchemes["basic"] = new()
        {
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            Scheme = "basic",
            Description = "Basic Authentication"
        };

        document.SecurityRequirements.Add(new()
        {
            [new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Id = "basic",
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme
                }
            }] = new List<string>()
        });

        return Task.CompletedTask;
    });
});

builder.Services.AddControllers();

builder.Services.AddDbContext<TodoApiDbContext>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    );
});

var app = builder.Build();

Log.Information("TodoApi starting up in {Environment} mode", app.Environment.EnvironmentName);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    
}

app.UseExceptionHandler();
app.UseRequestLoggingMiddleware();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();