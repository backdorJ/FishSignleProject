using System.Reflection;
using System.Text;
using FishShop.Core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace FishShop.API;

public static class Entry
{
    /// <summary>
    /// Добавить аутентификацию
    /// </summary>
    /// <param name="services">Сервисы</param>
    public static void AddCustomAuth(this IServiceCollection services, IConfiguration configuration)
        => services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = configuration["JwtOptions:Audience"],
                    ValidIssuer = configuration["JwtOptions:Issuer"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtOptions:Secret"]!))
                };
            });

    /// <summary>
    /// Добавить привязки настроек к классам
    /// </summary>
    /// <param name="services">Сервисы</param>
    /// <param name="configuration">Конфигурация</param>
    public static void AddBindOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
    }

    /// <summary>
    /// Конфигурация сваггера
    /// </summary>
    /// <param name="services">Сервисы</param>
    public static void AddSwaggerCustom(this IServiceCollection services)
        => services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                    new string[] { }
                }
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
}