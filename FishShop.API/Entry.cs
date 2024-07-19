using System.Text;
using FishShop.Core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtOptions:Secret"]!))
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
}