using Api.Repository;
using Api.Services;
using Api.Applications;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WatchDog;


var builder = WebApplication.CreateBuilder(args);
// Asegura que la ruta de configuración es correcta

builder.Services.AddWatchDogServices();

var misreglascors = "ReglaCors";
builder.Services.AddCors(option => option.AddPolicy(name: misreglascors, builder => { builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); }));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Layout", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Jwt Authorization",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

Console.WriteLine("Jwt Key: " + builder.Configuration["Jwt:Key"]);
Console.WriteLine("Jwt Issuer: " + builder.Configuration["Jwt:Issuer"]);
Console.WriteLine("Jwt Audience: " + builder.Configuration["Jwt:Audience"]);
Console.WriteLine("CadenaSQL desde appsettings: " + builder.Configuration.GetConnectionString("CadenaSQL"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]))
        };
        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = context =>
            {
                Console.WriteLine("Token validado exitosamente.");
                return Task.CompletedTask;
            },
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"Token inv�lido: {context.Exception.Message}");
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                Console.WriteLine($"Challenge generado: {context.ErrorDescription}");
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddRepositoryRegisters();
builder.Services.AddServicesServices();
builder.Services.AddApplicationsServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(misreglascors);
app.UseAuthentication();
app.UseAuthorization();

//watchdog logger
app.UseWatchDogExceptionLogger();
app.UseWatchDog(config =>
{
    config.WatchPageUsername = builder.Configuration.GetSection("WatchDog:Username").Value;
    config.WatchPagePassword = builder.Configuration.GetSection("WatchDog:Password").Value;
});

app.MapControllers();

app.Run();
